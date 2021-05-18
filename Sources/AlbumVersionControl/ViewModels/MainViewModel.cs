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
        public void CreateNewVersion()
        {
            var newCommitDialog = new NewCommitDialogView();
            newCommitDialog.ShowDialog();
            if (newCommitDialog.DialogResult != null && (bool)newCommitDialog.DialogResult)
            {
                Program.ProjectViewModel?.CreateNewVersion(newCommitDialog.Message);
            }
        }

        [Command]
        public void OpenVersionFolder()
        {
            Program.ProjectViewModel?.OpenCuerrentVersionFolder();
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
                Program.GitHubService.CreateProject(newProjectDialog.ProjectName, newProjectDialog.ProjectDescription);

                if (NavigationService.Current is ProjectJournalViewModel projectJournalViewModel)
                {
                    projectJournalViewModel.LoadProjects();
                }
            }
        }
    }
}