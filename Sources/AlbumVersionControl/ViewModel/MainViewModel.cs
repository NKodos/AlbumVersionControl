using AlbumVersionControl.Model;

namespace AlbumVersionControl.ViewModel
{
    public class MainViewModel : BaseVm
    {
        public MainViewModel()
        {
            OverlayService.GetInstance().Show = str => { OverlayService.GetInstance().Text = str; };
        }
    }
}