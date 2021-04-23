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

        public GitHubClient GetClient(IGitConnection connection)
        {
            var client = new GitHubClient(new ProductHeaderValue(ProjectName));

            switch (connection.ConnectionType)
            {
                case GitConnectionType.Guest:
                    break;
                case GitConnectionType.Basic:
                    client.Credentials = new Credentials(connection.Login, connection.Password, AuthenticationType.Basic);
                    break;
                case GitConnectionType.Oauth:
                    client.Credentials = new Credentials(connection.Token, AuthenticationType.Oauth);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var user = client.User.Current().Result;
            return client;
        }
    }
}