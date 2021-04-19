using System;
using Octokit;

namespace OctokitSamples
{
    public class Helper
    {
        public static void DeleteRepo(IConnection connection, Repository repository)
        {
            if (repository != null)
                DeleteRepo(connection, repository.Owner.Login, repository.Name);
        }

        public static void DeleteRepo(IConnection connection, string owner, string name)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.Repository.Delete(owner, name).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }
    }
}