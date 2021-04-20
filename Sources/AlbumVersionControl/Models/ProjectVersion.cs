using System;
using System.Collections.Generic;

namespace AlbumVersionControl.Models
{
    public class ProjectVersion
    {
        public Guid Id { get; set; }

        public Version Version { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Author { get; set; }

        public Project Project { get; set; }

        // repositoryId, commitSha
        public KeyValuePair<long, string> CommitDetail { get; set; }
    }
}