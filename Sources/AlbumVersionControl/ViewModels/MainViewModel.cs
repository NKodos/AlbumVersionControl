using DevExpress.Mvvm;

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
    }
}