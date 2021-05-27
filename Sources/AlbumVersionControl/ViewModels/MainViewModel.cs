using System.Configuration;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Extensions;
using AlbumVersionControl.Models.GitHubApi;
using AlbumVersionControl.View;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace AlbumVersionControl.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService NavigationService { get { return GetService<INavigationService>(); } }

        protected IWindowService WindowService { get { return GetService<IWindowService>(); } }

        public void OnViewLoaded()
        {
            InitializeGitHubService();
            LoadProjectJournalIfConnected();
        }

        [Command]
        public void CreateNewVersion()
        {
            var newCommitDialog = new NewCommitDialogView();
            newCommitDialog.ShowDialog();
            if (newCommitDialog.DialogResult != null && newCommitDialog.DialogResult.Value &&
                NavigationService.Current is ProjectViewModel projectViewModel)
            {
                projectViewModel.CreateNewVersion(newCommitDialog.Message);
            }
        }

        [Command]
        public void OpenVersionFolder()
        {
            if (NavigationService.Current is ProjectViewModel projectViewModel)
            {
                projectViewModel.OpenCuerrentVersionFolder();
            }
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
            var result = newProjectDialog.ShowDialog();
            if (result != null && result.Value)
            {
                Program.GitHubService.CreateProject(newProjectDialog.ProjectName, newProjectDialog.ProjectDescription);

                if (NavigationService.Current is ProjectJournalViewModel projectJournalViewModel)
                {
                    projectJournalViewModel.LoadProjects();
                }
            }
        }

        [Command]
        public void ShowAuthorizationWindow(string errorMessage = null)
        {
            var authorizationWindow = errorMessage == null
                ? new AuthorizationWindow()
                : new AuthorizationWindow(errorMessage);
            authorizationWindow.ShowDialog();

            if (authorizationWindow.IsConected)
            {
                LoadProjectJournal();
            }
        }

        [Command]
        public bool CheckGitHubConnection()
        {
            try
            {
                Program.GitHubService.Connect();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void InitializeGitHubService()
        {
            var section = (GitHubConnectionConfigSection)ConfigurationManager.GetSection("GitHubConnection");
            var connectionFromConfig = section.GitHubConnectionItems?[0];
            var connection = connectionFromConfig.ConvertToGitHubConnection();
            Program.GitHubService = new GitHubService(connection);
        }

        private void LoadProjectJournal()
        {
            NavigationService.Navigate("ProjectJournalView", null, this);
        }

        private void LoadProjectJournalIfConnected()
        {
            if (CheckGitHubConnection())
            {
                LoadProjectJournal();
            }
            else
            {
                ShowAuthorizationWindow("Ошибка подключения.\nИзмените параметры входа.");
            }
        }
    }
}