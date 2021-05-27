using System;
using System.Collections.Generic;
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

        public void SetProjects(List<Project> projects)
        {
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();
            Projects = new ObservableCollection<Project>();
        }

        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);

            if (parameter == null) parameter = new List<Project>();
            if (!(parameter is List<Project> projects))
                throw new ArgumentException("Parameter type unknown", nameof(parameter));

            SetProjects(projects);
        }

        private void NavigateProject(Project project)
        {
            NavigationService.Navigate("ProjectView", project, this);
        }
    }
}