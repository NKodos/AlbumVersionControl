using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlbumVersionControl.Configs;
using GitApi.Interfaces;
using Octokit;
using FileMode = Octokit.FileMode;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubRepository : GitHubObject, IGitRepository
    {
        private const string HeadMasterRef = "heads/main";

        public GitHubRepository(GitHubClient client)
        {
            Client = client;
        }

        public GitHubRepository(GitHubClient client, IGitOwner owner) : this(client)
        {
            Owner = owner;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IGitOwner Owner { get; set; }

        public IGitCommit CreateNewCommit(string message)
        {
            var masterReference =
                Client.Git.Reference.Get(Owner.Name, Name, HeadMasterRef).Result;
            var latestCommit = Client.Git.Commit.Get(Owner.Name, Name,
                masterReference.Object.Sha).Result;
            var nt = new NewTree { BaseTree = latestCommit.Tree.Sha };

            foreach (var filePath in Directory.GetFiles(new AppConfiguration().VersionContentFolder))
            {
                var textBlob = new NewBlob { Encoding = EncodingType.Utf8, Content = File.ReadAllText(filePath) };
                var textBlobRef = Client.Git.Blob.Create(Owner.Name, Name, textBlob);

                nt.Tree.Add(new NewTreeItem
                {
                    Path = Path.GetFileName(filePath),
                    Mode = FileMode.File,
                    Type = TreeType.Blob,
                    Sha = textBlobRef.Result.Sha
                });
            }

            var newTree = Client.Git.Tree.Create(Owner.Name, Name, nt).Result;
            var newCommit = new NewCommit(message, newTree.Sha, masterReference.Object.Sha);
            var commit = Client.Git.Commit.Create(Owner.Name, Name, newCommit).Result;
            
            var reference = Client.Git.Reference.Update(Owner.Name, Name, HeadMasterRef, new ReferenceUpdate(commit.Sha)).Result;
            return new GitHubCommit(Client, this)
            {
                Message = message,
                Author = commit.Author.Name,
                CreatedAt = commit.Author.Date.DateTime,
                Sha = commit.Sha
            };
        }

        public IGitCommit GetCommit(string reference)
        {
            var commit = Client.Repository.Commit.Get(Owner.Name, Name, reference).Result;
            var gitHubCommit = new GitHubCommit(Client, this);
            gitHubCommit.Map(commit);
            return gitHubCommit;
        }

        public IEnumerable<IGitCommit> GetAllCommits()
        {
            var commits = Client.Repository.Commit.GetAll(Owner.Name, Name).Result;
            return commits.Select(commit =>
            {
                var gitHubCommit = new GitHubCommit(Client, this);
                gitHubCommit.Map(commit);
                gitHubCommit.Repository = this;
                return gitHubCommit;
            }).ToList();
        }

        public void Map(Repository repository)
        {
            if (Owner == null)
            {
                Owner = new GitHubOwner(Client, repository.Owner.Name);
            }

            Id = repository.Id;
            Name = repository.Name;
            Description = repository.Description;
            CreatedAt = repository.CreatedAt.DateTime;
            UpdatedAt = repository.UpdatedAt.DateTime;
            Url = repository.Url;
        }
    }
}