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

        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();
            Projects = ProjectInitializer.GetProjectsDataSource();
        }

        private void NavigateProject(Project project)
        {
            NavigationService.Navigate("ProjectView", project, this);
        }
    }
}