using DevExpress.Mvvm;

namespace AlbumVersionControl.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

        }

        public string Title
        {
            get { return GetValue<string>(nameof(Title)); }
            set { SetValue(value, nameof(Title)); }
        }
    }
}