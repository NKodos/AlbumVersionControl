using System;
using System.Collections.Generic;

namespace AlbumVersionControl.Models
{
    public class Project
    {
        public string Title { get; set; }

        public string Caption { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Version> Versions { get; set; }
    }
}