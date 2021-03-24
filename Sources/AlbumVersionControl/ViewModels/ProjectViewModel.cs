using System;
using DevExpress.Mvvm;

namespace AlbumVersionControl.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ProjectViewModel()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}