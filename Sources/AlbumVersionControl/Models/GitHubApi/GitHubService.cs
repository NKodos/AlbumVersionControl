
using System.Collections.Generic;
using GitApi.Interfaces;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubService : GitServiceBase
    {
        public GitHubService(IGitConnection connection) : base(connection)
        {
        }

        public override void Connect()
        {
            Client = null;
            GetClient(Connection);
        }

        public void Connect(IGitConnection connection)
        {
            Connection = connection;
            Connect();
        }

        public IGitOwner GetCurrentOwner()
        {
            return GetOwner(Connection.Login);
        }

        public override IGitOwner GetOwner(string name)
        {
            return new GitHubOwner(Client, name, Connection);
        }

        public override IGitRepository GetRepository(long repositoryId)
        {
            var repository = Client.Repository.Get(repositoryId).Result;
            var owner = new GitHubOwner(Client, repository.Owner.Name, Connection);
            var gitHubRepository = new GitHubRepository(Client, owner);
            gitHubRepository.Map(repository);
            return gitHubRepository;
        }

        public override IGitCommit GetCommit(long repositoryId, string reference)
        {
            var commit = Client.Repository.Commit.Get(repositoryId, reference).Result;
            var gitHubCommit = new GitHubCommit(Client);
            gitHubCommit.Map(commit);
            return gitHubCommit;
        }

        public IReadOnlyList<RepositoryContent> GetRepositoryContent(long repositoryId, string reference)
        {
            return Client.Repository.Content.GetAllContentsByRef(repositoryId, reference).Result;
        }

        public void CreateProject(string name, string description)
        {
            var owner = GetCurrentOwner();
            owner.CreateRepository(name, description);
        }
    }
}