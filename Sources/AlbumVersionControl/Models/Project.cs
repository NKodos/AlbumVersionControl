using System;
using System.Collections.Generic;
using System.Linq;
using AlbumVersionControl.Extensions;
using GitApi.Interfaces;

namespace AlbumVersionControl.Models
{
    public class Project
    {
        public string Title { get; set; }

        public string Caption { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<ProjectVersion> Versions { get; set; }

        public IGitRepository GitRepository { get; set; }

        public List<ProjectVersion> GetProjectVersions()
        {
            var commits = GitRepository.GetAllCommits();
            return commits?.ConvertToVersions(this);
        }

        public static List<Project> GetFromGitRepositories(IEnumerable<IGitRepository> repositories)
        {
            return repositories.Select(rep =>
            {
                var project = new Project();
                project.Map(rep);
                return project;
            }).ToList();
        }
    }
}