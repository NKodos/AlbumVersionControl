using System.Linq;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Models.GitHubApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlbumVersionControlTests.GitHubTests
{
    [TestClass]
    public class GitHubFastTests
    {
        [TestMethod]
        public void FastTest()
        {
            var connection = new GitHubConnection("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");
            var service = new GitHubService(connection);
            service.Connect();
            var owner = service.GetOwner("kodboss");
            var repositories = owner.GetRepositories().ToList();
            var repository = owner.GetRepository("Customers");
            var commits = repository.GetAllCommits();
            var commit = repository.GetCommit("e0da3edc66aac44348fe0837a878a1019ed51ca2");
        }   
    }
}
