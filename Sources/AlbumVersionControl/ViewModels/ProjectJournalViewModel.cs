using System.Collections.ObjectModel;
using AlbumVersionControl.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace AlbumVersionControl.ViewModels
{
    public class ProjectJournalViewModel : ViewModelBase
    {
        private INavigationService NavigationService
        {
            get { return GetService<INavigationService>(); }
        }

        public ObservableCollection<Project> Projects
        {
            get { return GetValue<ObservableCollection<Project>>(nameof(Projects)); }
            set { SetValue(value, nameof(Projects)); }
        }

        [Command]
        public void RowDoubleClick(Project currentItem)
        {
            NavigateProject(currentItem);
        }

        public void LoadProjects()
        {
            var gitHubRepositories = Program.GitHubService.GetCurrentOwner().GetRepositories();
            Projects = new ObservableCollection<Project>(Project.GetFromGitRepositories(gitHubRepositories));
        }

        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();
            LoadProjects();
        }

        private void NavigateProject(Project project)
        {
            NavigationService.Navigate("ProjectView", project, this);
        }
    }
}