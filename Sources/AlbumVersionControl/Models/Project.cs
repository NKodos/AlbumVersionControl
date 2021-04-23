using System;
using System.Collections.Generic;
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
    }
}