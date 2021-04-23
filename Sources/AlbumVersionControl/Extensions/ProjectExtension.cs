using AlbumVersionControl.Models;
using DevExpress.XtraPrinting.Native;
using GitApi.Interfaces;

namespace AlbumVersionControl.Extensions
{
    public static class ProjectExtension
    {
        public static void Map(this Project project, IGitRepository repository)
        {
            project.Title = repository.Name;
            project.Caption = repository.Description;
            project.CreatedAt = repository.CreatedAt;
            project.UpdatedAt = repository.UpdatedAt;
            project.Versions = repository.GetAllCommits().ConvertToVersions(project);
        }
    }
}