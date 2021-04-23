using GitApi;
using GitApi.Interfaces;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubConnection : GitHubObject, IGitConnection
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public GitConnectionType ConnectionType { get; set; }

        public GitHubConnection()
        {
            ConnectionType = GitConnectionType.Guest;
        }

        public GitHubConnection(string login, string password)
        {
            Login = login;
            Password = password;
            ConnectionType = GitConnectionType.Basic;
        }

        public GitHubConnection(string token)
        {
            Token = token;
            ConnectionType = GitConnectionType.Oauth;
        }
    }
}