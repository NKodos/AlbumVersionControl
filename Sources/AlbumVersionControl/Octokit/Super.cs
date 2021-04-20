using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using AlbumVersionControl.Models;
using Octokit;
using Project = AlbumVersionControl.Models.Project;

namespace AlbumVersionControl.Octokit
{
    public static class SuperGlobal
    {
        private const string Owner = "kodboss";

        public const string FolderPath = @"C:\Users\User\Desktop\VersionContent";

        public static GitHubClient GetClient()
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var basicAuth =
                new Credentials(
                    "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"); // "kodboss", "147258github"  "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"
            client.Credentials = basicAuth;
            return client;
        }

        public static List<Repository> GetRepositories(GitHubClient client)
        {
            var repository = client.Repository.GetAllForCurrent().Result;
            return repository.ToList();
        }

        public static List<Project> GetProjectsFromGitHub(GitHubClient client)
        {
            var repositories = GetRepositories(client);
            return repositories.RepositoryToProject(client);
        }

        public static List<Project> RepositoryToProject(this List<Repository> repositories, GitHubClient client)
        {
            return repositories.Select(rep =>
            {
                var project = new Project
                {
                    Title = rep.Name,
                    Caption = rep.Description,
                    CreatedAt = rep.CreatedAt.DateTime,
                    UpdatedAt = rep.UpdatedAt.DateTime
                };
                project.Versions = rep.GetCommits(client).Commits2Versions(rep, project);
                return project;
            }).ToList();
        }


        public static IReadOnlyList<GitHubCommit> GetCommits(this Repository repository, GitHubClient client)
        {
            return client.Repository.Commit.GetAll(Owner, repository.Name).Result;
        }

        public static List<ProjectVersion> Commits2Versions(this IReadOnlyList<GitHubCommit> commits, Repository repository, 
            Project project = null)
        {
            var version = commits.Count;
            return commits.Select(commit => new ProjectVersion
            {
                Description = commit.Commit.Message,
                Author = commit.Author.Login,
                Version = new Version("1.0." + version--),
                CommitDetail = new KeyValuePair<long, string>(repository.Id, commit.Sha),
                Project = project
            }).ToList();
        }

        public static void LoadCommitFile(KeyValuePair<long, string> commitDetails)
        {
            ClearFolder();

            var client = GetClient();
            var commit = client.Repository.Commit.Get(commitDetails.Key, commitDetails.Value).Result;

            foreach (var file in commit.Files)
            {
                DownloadFile(file);
            }
        }

        private static void DownloadFile(GitHubCommitFile file)
        {
            using (var client = new WebClient())
            {
                var fileName = Path.GetFileName(file.Filename);
                client.DownloadFile(file.RawUrl, $"{FolderPath}/{fileName}");
            }
        }

        private static void ClearFolder()
        {
            var di = new DirectoryInfo(FolderPath);

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}