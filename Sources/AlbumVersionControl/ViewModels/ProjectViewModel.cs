using System;
using AlbumVersionControl.Models;
using DevExpress.Mvvm;

namespace AlbumVersionControl.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public Project CurrentProject
        {
            get { return GetProperty(() => CurrentProject); }
            private set { SetProperty(() => CurrentProject, value); }
        }

        public ProjectViewModel()
        {

        }

        protected override void OnParameterChanged(object parameter)
        {
            if (parameter == null) parameter = new Project();
            if (!(parameter is Project currentProject)) throw new ArgumentException("Parameter type unknown", nameof(parameter));
            CurrentProject = currentProject;
            base.OnParameterChanged(parameter);
        }
    }
}