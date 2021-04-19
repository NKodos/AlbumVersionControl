using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.WindowsUI;

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
        public void OpenVersionFolder()
        {
            Global.ProjectViewModel?.OpenCuerrentVersionFolder();
        }

        [Command]
        public void GenerateClasses()
        {
            Generator.Generate();
        }
    }
}