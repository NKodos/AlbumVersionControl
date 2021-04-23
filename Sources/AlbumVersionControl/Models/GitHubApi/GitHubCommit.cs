using System;
using GitApi.Interfaces;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubCommit : GitHubObject, IGitCommit
    {
        public GitHubCommit()
        {
        }

        public GitHubCommit(IGitRepository repository)
        {
            Repository = repository;
        }

        public GitHubCommit(string message, string sha) : this()
        {
            Message = message;
            Sha = sha;
        }

        public GitHubCommit(string message, string sha, IGitRepository repository) : this(message, sha)
        {
            Repository = repository;
        }

        public string Message { get; set; }

        public string Sha { get; set; }

        public string Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public IGitRepository Repository { get; set; }

        public void Map(global::Octokit.GitHubCommit commit)
        {
            if (Repository == null)
            {
                Repository = new GitHubRepository();
                if (Repository is GitHubRepository gitHubRepository)
                {
                    gitHubRepository.Map(commit.Repository);
                }
            }

            Message = commit.Commit.Message;
            Sha = commit.Sha;
            Author = commit.Author.Login;
            CreatedAt = commit.Commit.Committer.Date.DateTime;
        }
    }
}