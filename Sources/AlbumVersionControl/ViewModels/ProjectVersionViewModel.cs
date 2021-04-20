using System;
using AlbumVersionControl.Models;
using DevExpress.Mvvm;

namespace AlbumVersionControl.ViewModels
{
    public class ProjectVersionViewModel : ViewModelBase
    {
        public ProjectVersion CurrentVersion
        {
            get { return GetProperty(() => CurrentVersion); }
            private set { SetProperty(() => CurrentVersion, value); }
        }

        protected override void OnParameterChanged(object parameter)
        {
            if (parameter == null) parameter = new ProjectVersion();
            if (!(parameter is ProjectVersion currentProjectVersion)) throw new ArgumentException("Parameter type unknown", nameof(parameter));
            CurrentVersion = currentProjectVersion;

            var t = CurrentVersion.Author;

            base.OnParameterChanged(parameter);
        }
    }
}