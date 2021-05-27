using System;
using GitApi;
using GitApi.Interfaces;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubObject
    {
        // Todo: сделать эту константу генерируемую из каких-то идентифицирующихся элементов
        public const string ProjectName = "AlbumVersionControl";
        private GitHubClient _client;

        public GitHubClient GetClient(IGitConnection connection)
        {
            try
            {
                if (_client != null) return _client;

                CreatNewClient(connection);

                var user = connection.ConnectionType == GitConnectionType.Guest 
                    ?_client.User.Get(connection.Login).Result
                    :_client.User.Current().Result;

                if (connection.ConnectionType == GitConnectionType.Oauth)
                {
                    connection.Login = user.Login;
                }

                return _client;
            }
            catch
            {
                _client = null;
                throw;
            }
        }

        protected virtual GitHubClient CreatNewClient(IGitConnection connection)
        {
            _client = new GitHubClient(new ProductHeaderValue(ProjectName));

            switch (connection.ConnectionType)
            {
                case GitConnectionType.Guest:
                    break;
                case GitConnectionType.Basic:
                    _client.Credentials =
                        new Credentials(connection.Login, connection.Password, AuthenticationType.Basic);
                    break;
                case GitConnectionType.Oauth:
                    _client.Credentials = new Credentials(connection.Token, AuthenticationType.Oauth);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _client;
        }
    }
}