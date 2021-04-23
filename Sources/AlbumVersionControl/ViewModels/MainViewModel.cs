using AlbumVersionControl.Models;
using AlbumVersionControl.View;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace AlbumVersionControl.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService NavigationService { get { return this.GetService<INavigationService>(); } }

        public MainViewModel()
        {
            
        }

        public void OnViewLoaded()
        {
            NavigationService.Navigate("ProjectJournalView", null, this);
        }

        [Command]
        public void OpenVersionFolder()
        {
            Global.ProjectViewModel?.OpenCuerrentVersionFolder();
        }

        [Command]
        public void GenerateClasses()
        {
            Generator.Generate();
        }

        [Command]
        public void CreateProject()
        {
            var newProjectDialog = new NewProjcetDialogView();
            newProjectDialog.ShowDialog();
            if (newProjectDialog.DialogResult != null && (bool)newProjectDialog.DialogResult)
            {
                var journal = new AlbumJournal();
                journal.CreateProject(newProjectDialog.ProjectName, newProjectDialog.ProjectDescription);

                if (NavigationService.Current is ProjectJournalViewModel projectJournalViewModel)
                {
                    projectJournalViewModel.LoadProjects();
                }
            }
        }
    }
}