using System.Text.RegularExpressions;
using AlbumVersionControl.Models;
using DevExpress.XtraPrinting.Native;
using GitApi.Interfaces;

namespace AlbumVersionControl.Extensions
{
    public static class ProjectExtension
    {
        public static void Map(this Project project, IGitRepository repository)
        {
            project.Title = GetRusProjectName(repository.Description);
            project.Caption = GetProjectDescription(repository.Description);
            project.Tag = repository.Name;
            project.CreatedAt = repository.CreatedAt;
            project.UpdatedAt = repository.UpdatedAt;
            project.GitRepository = repository;
        }

        private static string GetRusProjectName(string description)
        {
            if (description == null) return string.Empty;
            var rgx = new Regex(@"^#[\s,\w]*///");
            var matchValue = rgx.Match(description).Value;
            return !string.IsNullOrWhiteSpace(matchValue) 
                ? matchValue.Substring(1, matchValue.Length - 4)
                : matchValue;
        }

        private static string GetProjectDescription(string description)
        {
            if (description == null) return string.Empty;
            var rgx = new Regex(@"^#[\s,\w]*///");
            var matchValue = rgx.Match(description).Value;
            return string.IsNullOrWhiteSpace(matchValue)
                ? description
                : description.Replace(matchValue, "");
        }
    }
}