using AlbumVersionControl.Configs;
using AlbumVersionControl.Models.GitHubApi;

namespace AlbumVersionControl.Extensions
{
    public static class GitHubConnectionExtension
    {
        public static GitHubConnection ConvertToGitHubConnection(this GitHubConnectionElement configConnection)
        {
            if (!string.IsNullOrEmpty(configConnection.Token))
            {
                return new GitHubConnection(configConnection.Token);
            }

            return !string.IsNullOrEmpty(configConnection.Password)
                ? new GitHubConnection(configConnection.Login, configConnection.Password)
                : new GitHubConnection();
        }
    }
}