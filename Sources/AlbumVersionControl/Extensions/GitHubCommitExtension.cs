using System;
using System.Collections.Generic;
using System.Linq;
using AlbumVersionControl.Models;
using GitApi.Interfaces;
using Project = AlbumVersionControl.Models.Project;

namespace AlbumVersionControl.Extensions
{
    public static class GitHubCommitExtension
    {
        public static List<ProjectVersion> ConvertToVersions(this IEnumerable<IGitCommit> commits, Project project = null)
        {
            var gitCommits = commits.ToList();
            var version = gitCommits.Count;
            return gitCommits.Select(commit => new ProjectVersion
            {
                Description = commit.Message,
                Author = commit.Author,
                Version = new Version("1.0." + version--),
                CommitDetail = new KeyValuePair<long, string>(commit.Repository.Id, commit.Sha),
                Project = project,
                CreatedAt = commit.CreatedAt,
                GitCommit = commit
            }).ToList();
        }
    }
}