using System;
using System.Collections.Generic;
using System.Linq;
using GitApi.Interfaces;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubRepository : GitHubObject, IGitRepository
    {
        public GitHubRepository()
        {
            
        }

        public GitHubRepository(IGitOwner owner) : this()
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

        public IGitCommit GetCommit(string reference)
        {
            var client = GetClient(Owner.Connection);
            var commit = client.Repository.Commit.Get(Owner.Name, Name, reference).Result;
            var gitHubCommit = new GitHubCommit(this);
            gitHubCommit.Map(commit);
            return gitHubCommit;
        }

        public IEnumerable<IGitCommit> GetAllCommits()
        {
            var client = GetClient(Owner.Connection);
            var commits = client.Repository.Commit.GetAll(Owner.Name, Name).Result;
            return commits.Select(commit =>
            {
                var gitHubCommit = new GitHubCommit(this);
                gitHubCommit.Map(commit);
                gitHubCommit.Repository = this;
                return gitHubCommit;
            }).ToList();
        }

        public void Map(Repository repository)
        {
            if (Owner == null)
            {
                Owner = new GitHubOwner(repository.Owner.Name);
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