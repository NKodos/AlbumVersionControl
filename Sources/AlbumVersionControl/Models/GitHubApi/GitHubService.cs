using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var client = GetClient(Connection);
            var user = client.User.Current().Result;
        }

        public override IGitOwner GetOwner(string name)
        {
            var client = GetClient(Connection);
            var user = client.User.Current().Result;
            return new GitHubOwner(user.Login, Connection);
        }

        public override IGitRepository GetRepository(long repositoryId)
        {
            var client = GetClient(Connection);
            var repository = client.Repository.Get(repositoryId).Result;
            var owner = new GitHubOwner(repository.Owner.Name, Connection);
            var gitHubRepository = new GitHubRepository(owner);
            gitHubRepository.Map(repository);
            return gitHubRepository;
        }

        public override IGitCommit GetCommit(long repositoryId, string reference)
        {
            var client = GetClient(Connection);
            var commit = client.Repository.Commit.Get(repositoryId, reference).Result;
            var gitHubCommit = new GitHubCommit();
            gitHubCommit.Map(commit);
            return gitHubCommit;
        }

        public IReadOnlyList<RepositoryContent> GetRepositoryContent(long repositoryId, string reference)
        {
            var client = GetClient(Connection);
            return client.Repository.Content.GetAllContentsByRef(repositoryId, reference).Result;
        }

        public IGitOwner GetCurrentOwner()
        {
            return GetOwner(Connection.Login);
        }

        public void CreateProject(string name, string description)
        {
            var owner = GetCurrentOwner();
            owner.CreateRepository(name, description);
        }
    }
}