using System.Collections.Generic;
using System.Linq;
using GitApi.Interfaces;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubOwner : GitHubObject, IGitOwner
    {
        public GitHubOwner(GitHubClient client)
        {
            Client = client;
        }

        public GitHubOwner(GitHubClient client, string name) : this(client)
        {
            Name = name;
        }

        public GitHubOwner(GitHubClient client, string name, IGitConnection connection) : this(client, name)
        {
            Connection = connection;
        }

        public string Name { get; set; }

        public IGitConnection Connection { get; set; }

        public IGitRepository GetRepository(string name)
        {
            var repository = Client.Repository.Get(Name, name).Result;
            var gitHubRepository = new GitHubRepository(Client, this);
            gitHubRepository.Map(repository);
            return gitHubRepository;
        }

        public IGitRepository CreateRepository(string name, string description)
        {
            var newRepository = new NewRepository(name) { AutoInit = true, Description = description };
            var repository = Client.Repository.Create(newRepository).Result;
            var gitHubRepository = new GitHubRepository(Client, this);
            gitHubRepository.Map(repository);
            return gitHubRepository;
        }

        public IEnumerable<IGitRepository> GetRepositories()
        {
            var repositoryList = Client.Repository.GetAllForUser(Connection.Login).Result;
            return repositoryList.ToList().Select(repository =>
            {
                var gitHubRepository = new GitHubRepository(Client, this);
                gitHubRepository.Map(repository);
                return gitHubRepository;
            });
        }
    }
}