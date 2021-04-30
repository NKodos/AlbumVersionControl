using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Extensions;
using AlbumVersionControl.Models.GitHubApi;
using GitApi.Interfaces;
using Octokit;

namespace AlbumVersionControl.Models
{
    public class AlbumJournal
    {
        public readonly string VersionContentFolder;

        private GitHubConnection _connection;

        public AlbumJournal()
        {
            VersionContentFolder = new AppConfiguration().VersionContentFolder;
        }

        public List<Project> GetAllCurrentProjects()
        {
            GetConnectionFromConfig();
            var service = new GitHubService(_connection);
            var owner = service.GetOwner(_connection.Login);
            var repositories = owner.GetRepositories();
            return ConvertGitHubRepositoriesToProject(repositories);
        }

        public void LoadFiles(long repositoryId, string reference)
        {
            ClearFolder();
            GetConnectionFromConfig();
            var service = new GitHubService(_connection);
            foreach (var content in service.GetRepositoryContent(repositoryId, reference))
            {
                DownloadFile(content);
            }
        }

        public void CreateProject(string name, string description)
        {
            GetConnectionFromConfig();
            var service = new GitHubService(_connection);
            var owner = service.GetOwner(_connection.Login);
            owner.CreateRepository(name, description);
        }

        private static List<Project> ConvertGitHubRepositoriesToProject(IEnumerable<IGitRepository> repositories)
        {
            return repositories.Select(rep =>
            {
                var project = new Project();
                project.Map(rep);
                return project;
            }).ToList();
        }

        private void GetConnectionFromConfig()
        {
            var section = (GitHubConnectionConfigSection)ConfigurationManager.GetSection("GitHubConnection");
            var connection = section.GitHubConnectionItems?[0];
            _connection = ConvertConfigConnectionToGitHubConnection(connection);

        }

        private GitHubConnection ConvertConfigConnectionToGitHubConnection(GitHubConnectionElement configConnection)
        {
            if (!string.IsNullOrEmpty(configConnection.Token))
            {
                return new GitHubConnection(configConnection.Token);
            }

            return !string.IsNullOrEmpty(configConnection.Password)
                ? new GitHubConnection(configConnection.Login, configConnection.Password)
                : new GitHubConnection();
        }

        public List<ProjectVersion> GetProjectVersions(Project currentProject)
        {
            var commits = currentProject.GitRepository.GetAllCommits();
            return commits?.ConvertToVersions(currentProject);
        }

        private void DownloadFile(RepositoryContent content)
        {
            if (content.DownloadUrl == null) return;
            using (var client = new WebClient())
            {
                var fileName = Path.GetFileName(content.Name);
                client.DownloadFile(content.DownloadUrl, $"{VersionContentFolder}/{fileName}");
            }
        }

        private void ClearFolder()
        {
            var di = new DirectoryInfo(VersionContentFolder);

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}