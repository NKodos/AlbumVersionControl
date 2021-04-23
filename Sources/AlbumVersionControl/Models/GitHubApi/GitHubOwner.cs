using System.Collections.Generic;
using System.Linq;
using GitApi.Interfaces;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubOwner : GitHubObject, IGitOwner
    {
        public GitHubOwner()
        {
        }

        public GitHubOwner(string name) : this()
        {
            Name = name;
        }

        public GitHubOwner(string name, IGitConnection connection) : this(name)
        {
            Connection = connection;
        }

        public string Name { get; set; }

        public IGitConnection Connection { get; set; }

        public IGitRepository GetRepository(string name)
        {
            var client = GetClient(Connection);
            var repository = client.Repository.Get(Name, name).Result;
            var gitHubRepository = new GitHubRepository(this);
            gitHubRepository.Map(repository);
            return gitHubRepository;
        }

        public IGitRepository CreateRepository(string name, string description)
        {
            var newRepository = new NewRepository(name) { AutoInit = true, Description = description };
            var client = GetClient(Connection);
            var repository = client.Repository.Create(newRepository).Result;
            var gitHubRepository = new GitHubRepository(this);
            gitHubRepository.Map(repository);
            return gitHubRepository;
        }

        public IEnumerable<IGitRepository> GetRepositories()
        {
            var client = GetClient(Connection);
            var repositoryList = client.Repository.GetAllForCurrent().Result;
            return repositoryList.ToList().Select(repository =>
            {
                var gitHubRepository = new GitHubRepository(this);
                gitHubRepository.Map(repository);
                return gitHubRepository;
            });
        }
    }
}