﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using FileMode = Octokit.FileMode;

namespace OctokitSamples
{
    internal class Program
    {
        private const string Owner = "kodboss";

        private static async Task Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var basicAuth =
                new Credentials(
                    "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"); // "kodboss", "147258github"  "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"
            client.Credentials = basicAuth;
            var user = await client.User.Get(Owner);
            //CreateRepository(client);
            //GetAllCommits(client);
            //CreateMainBranch(client);
            CreateCommit(client);
        }

        private static void CreateRepository(IGitHubClient client)
        {
            var newRepository = new NewRepository("MyNewRepository11") {AutoInit = true};
            var a = client.Repository.Create(newRepository).Result;
        }

        private static void GetAllCommits(IGitHubClient client)
        {
            var repository = client.Repository.Get(Owner, "AlbumVersionControl").Result;

            var gitHubCommits = client.Repository.Commit.GetAll("kodboss", "AlbumVersionControl").Result;

            var commitsFiltered = gitHubCommits.Select(async (_) =>
            {
                return await client.Repository.Commit.Get("kodboss", "AlbumVersionControl", _.Sha);
            }).ToList();

            var commits = Task.WhenAll(commitsFiltered).Result;
        }

        private static void CreateMainBranch(GitHubClient client)
        {
            const string repositoryName = "MyNewRepository";
            var repository = client.Repository.Get(Owner, repositoryName).Result;
            var repositoryContext = new RepositoryContext(client.Connection, repository);

            var contents = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("READ.ME", "##Meow")
            };
            var r = CreateTree(client, repositoryContext, contents).Result;
        }

        private static void CreateCommit(GitHubClient client)
        {
            const string repositoryName = "Newnew111";

            // 1. Get the SHA of the latest commit of the master branch.
            var headMasterRef = "heads/main";
            var fileName = "Test.txt";
            var masterReference =
                client.Git.Reference.Get(Owner, repositoryName, headMasterRef).Result; // Get reference of master branch
            var latestCommit = client.Git.Commit.Get(Owner, repositoryName,
                masterReference.Object.Sha).Result; // Get the laster commit of this branch

            //2. Create the blob(s) corresponding to your file(s)
            var textBlob = new NewBlob {Encoding = EncodingType.Utf8, Content = File.ReadAllText(fileName) };
            var textBlobRef = client.Git.Blob.Create(Owner, repositoryName, textBlob);

            // 3. Create a new tree with:
            var nt = new NewTree{ BaseTree = latestCommit.Tree.Sha };
            nt.Tree.Add(new NewTreeItem
            {
                Path = fileName, Mode = FileMode.File, Type = TreeType.Blob, Sha = textBlobRef.Result.Sha
            });
            var newTree = client.Git.Tree.Create(Owner, repositoryName, nt).Result;

            // 4. Create the commit with the SHAs of the tree and the reference of master branch
            // Create Commit
            var newCommit = new NewCommit("Update Test.txt", newTree.Sha, masterReference.Object.Sha);
            var commit = client.Git.Commit.Create(Owner, repositoryName, newCommit).Result;

            // 5. Update the reference of master branch with the SHA of the commit
            // Update HEAD with the commit
            var reference = client.Git.Reference.Update(Owner, repositoryName, headMasterRef, new ReferenceUpdate(commit.Sha)).Result;
        }


        static async Task<TreeResponse> CreateTree(IGitHubClient github, RepositoryContext context, IEnumerable<KeyValuePair<string, string>> treeContents)
        {
            var collection = new List<NewTreeItem>();

            foreach (var c in treeContents)
            {
                var baselineBlob = new NewBlob
                {
                    Content = c.Value,
                    Encoding = EncodingType.Utf8
                };
                var baselineBlobResult = await github.Git.Blob.Create(context.RepositoryOwner, context.RepositoryName, baselineBlob);

                collection.Add(new NewTreeItem
                {
                    Type = TreeType.Blob,
                    Mode = FileMode.File,
                    Path = c.Key,
                    Sha = baselineBlobResult.Sha
                });
            }

            var newTree = new NewTree(){BaseTree = "4b825dc642cb6eb9a060e54bf8d69288fbee4904"};
            foreach (var item in collection)
            {
                newTree.Tree.Add(item);
            }

            return await github.Git.Tree.Create(context.RepositoryOwner, context.RepositoryName, newTree);
        }
    }
}