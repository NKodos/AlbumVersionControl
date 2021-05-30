using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Models;
using AlbumVersionControl.Models.GitHubApi;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace AlbumVersionControl.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private INavigationService NavigationService
        {
            get { return GetService<INavigationService>(); }
        }

        public Project CurrentProject
        {
            get { return GetProperty(() => CurrentProject); }
            private set { SetProperty(() => CurrentProject, value); }
        }

        public ProjectVersion SelectedVersion
        {
            get { return GetProperty(() => SelectedVersion); }
            private set { SetProperty(() => SelectedVersion, value); }
        }

        public ObservableCollection<ProjectVersion> Versions
        {
            get { return GetValue<ObservableCollection<ProjectVersion>>(nameof(Versions)); }
            set { SetValue(value, nameof(Versions)); }
        }

        protected override void OnParameterChanged(object parameter)
        {
            if (parameter == null) parameter = new Project();
            if (!(parameter is Project currentProject))
                throw new ArgumentException("Parameter type unknown", nameof(parameter));
            
            CurrentProject = currentProject;
            LoadVersions();

            base.OnParameterChanged(parameter);
        }

        [Command]
        public void RowDoubleClick(ProjectVersion currentVersion)
        {
            NavigateProject(currentVersion);
        }

        [Command]
        public void SelectedItem(ProjectVersion currentVersion)
        {
            SelectedVersion = currentVersion;
        }

        public void CreateNewVersion(string message)
        {
            if (SelectedVersion != null)
            {
                CurrentProject.GitRepository.CreateNewCommit(message);
                LoadVersions();
            }
        }

        private void NavigateProject(ProjectVersion version)
        {
            NavigationService.Navigate("ProjectVersionView", version, this);
        }

        private void LoadVersions()
        {
            var versions = CurrentProject.GetProjectVersions();
            Versions = new ObservableCollection<ProjectVersion>(versions);
        }
    }
}