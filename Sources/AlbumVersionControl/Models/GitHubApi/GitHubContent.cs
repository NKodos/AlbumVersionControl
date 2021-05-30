using System.IO;
using System.Net;
using AlbumVersionControl.Configs;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubContent
    {
        public GitHubService GitHubService { get; set; }

        public long RepositoryId { get; set; }

        public string Reference { get; set; }

        public GitHubContent(GitHubService service, long repositoryId, string reference)
        {
            GitHubService = service;
            RepositoryId = repositoryId;
            Reference = reference;
        }

        public void ClearFolder()
        {
            var versionContentFolder = new AppConfiguration().VersionContentFolder;
            var di = new DirectoryInfo(versionContentFolder);

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }
        }

        public void DownloadFiles()
        {
            foreach (var content in GitHubService.GetRepositoryContent(RepositoryId, Reference))
            {
                DownloadFile(content);
            }
        }

        public void DownloadFilesForDirectory(long repositoryId, string reference, string path)
        {
            foreach (var content in GitHubService.GetRepositoryContent(RepositoryId, Reference, path))
            {
                DownloadFile(content);
            }
        }

        private void DownloadFile(RepositoryContent content)
        {
            using (var client = new WebClient())
            {
                if (content.Type == new StringEnum<ContentType>(ContentType.Dir))
                {
                    CreateDirectory(content.Path);
                    DownloadFilesForDirectory(RepositoryId, Reference, content.Path);
                }
                else if (content.Type == new StringEnum<ContentType>(ContentType.File))
                {
                    var versionContentFolder = new AppConfiguration().VersionContentFolder;
                    client.DownloadFile(content.DownloadUrl, $"{versionContentFolder}/{content.Path}");
                }

            }
        }

        private void CreateDirectory(string path)
        {
            var versionContentFolder = new AppConfiguration().VersionContentFolder;
            var directoryPath = $"{versionContentFolder}/{path}";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}