using System.IO;
using System.Net;
using AlbumVersionControl.Configs;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubContent
    {
        public GitHubService GitHubService { get; set; }

        public GitHubContent(GitHubService service)
        {
            GitHubService = service;
        }

        public void LoadFiles(long repositoryId, string reference)
        {
            ClearFolder();

            foreach (var content in GitHubService.GetRepositoryContent(repositoryId, reference))
            {
                DownloadFile(content);
            }
        }

        private static void DownloadFile(RepositoryContent content)
        {
            if (content.DownloadUrl == null) return;
            using (var client = new WebClient())
            {
                var versionContentFolder = new AppConfiguration().VersionContentFolder;
                var fileName = Path.GetFileName(content.Name);
                client.DownloadFile(content.DownloadUrl, $"{versionContentFolder}/{fileName}");
            }
        }

        private static void ClearFolder()
        {
            var versionContentFolder = new AppConfiguration().VersionContentFolder;
            var di = new DirectoryInfo(versionContentFolder);

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}