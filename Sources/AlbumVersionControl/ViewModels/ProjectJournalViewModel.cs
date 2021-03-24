using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace AlbumVersionControl.ViewModels
{
    public class ProjectJournalViewModel : ViewModelBase
    {
        private INavigationService NavigationService { get { return this.GetService<INavigationService>(); } }

        public ProjectJournalViewModel() { }

        [Command]
        public void NavigateProject()
        {
            NavigationService.Navigate("ProjectView", null, this);
        }
    }
}