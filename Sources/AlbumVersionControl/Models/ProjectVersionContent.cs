using System.Collections.Generic;
using System.IO;
using Octokit;

namespace AlbumVersionControl.Models
{
    public class ProjectVersionContent
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public int Size { get; set; }

        public List<ProjectVersionContent> InnerContents { get; set; }

        public void Map(RepositoryContent repositoryContent)
        {
            FileName = Path.GetFileNameWithoutExtension(repositoryContent.Name);
            Extension = Path.GetExtension(repositoryContent.Name);
            Size = repositoryContent.Size;
        }
    }
}