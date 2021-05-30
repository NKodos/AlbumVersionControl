using System.Configuration;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Extensions;
using AlbumVersionControl.Models;
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

        public bool IsJournalViewShown
        {
            get { return GetValue<bool>(nameof(IsJournalViewShown)); }
            set { SetValue(value, nameof(IsJournalViewShown)); }
        }

        public bool IsProjectViewShown
        {
            get { return GetValue<bool>(nameof(IsProjectViewShown)); }
            set { SetValue(value, nameof(IsProjectViewShown)); }
        }

        public bool IsProjectVersionViewShown
        {
            get { return GetValue<bool>(nameof(IsProjectVersionViewShown)); }
            set { SetValue(value, nameof(IsProjectVersionViewShown)); }
        }

        public void OnViewLoaded()
        {
            InitializeGitHubService();
            LoadProjectJournalIfConnected();
        }

        [Command]
        public void OnViewContentRendered()
        {
            var currentView = NavigationService.Current;
            IsJournalViewShown = currentView is ProjectJournalViewModel;
            IsProjectViewShown = currentView is ProjectViewModel;
            IsProjectVersionViewShown = currentView is ProjectVersionViewModel;
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
            if (NavigationService.Current is ProjectVersionViewModel projectVersionViewModel)
            {
                projectVersionViewModel.OpenCuerrentVersionFolder();
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
                LoadProjectJournal();
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
            var gitHubRepositories = Program.GitHubService.GetCurrentOwner().GetRepositories();
            var projects = Project.GetFromGitRepositories(gitHubRepositories);
            NavigationService.Navigate("ProjectJournalView", projects, this);
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