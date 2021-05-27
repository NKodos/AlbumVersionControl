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

        protected GitHubClient Client;

        public GitHubClient GetClient(IGitConnection connection)
        {
            try
            {
                if (Client != null) return Client;

                CreatNewClient(connection);

                var user = connection.ConnectionType == GitConnectionType.Oauth 
                    ? Client.User.Current().Result
                    : Client.User.Get(connection.Login).Result;

                if (connection.ConnectionType == GitConnectionType.Oauth)
                {
                    connection.Login = user.Login;
                }

                return Client;
            }
            catch
            {
                Client = null;
                throw;
            }
        }

        protected virtual GitHubClient CreatNewClient(IGitConnection connection)
        {
            Client = new GitHubClient(new ProductHeaderValue(ProjectName));

            switch (connection.ConnectionType)
            {
                case GitConnectionType.Guest:
                    break;
                case GitConnectionType.Basic:
                    Client.Credentials =
                        new Credentials(connection.Login, connection.Password, AuthenticationType.Basic);
                    break;
                case GitConnectionType.Oauth:
                    Client.Credentials = new Credentials(connection.Token, AuthenticationType.Oauth);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Client;
        }
    }
}