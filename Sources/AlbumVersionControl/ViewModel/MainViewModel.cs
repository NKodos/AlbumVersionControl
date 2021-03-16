using DevExpress.Mvvm;

namespace AlbumVersionControl.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            var testVm = new TestViewModel();
            ((ISupportParameter) testVm).Parameter = "Doc_1";
        }

        public string Title
        {
            get { return GetValue<string>(nameof(Title)); }
            set { SetValue(value, nameof(Title)); }
        }
    }
}