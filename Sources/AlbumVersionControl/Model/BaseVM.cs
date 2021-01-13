using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlbumVersionControl.Model
{
    public class BaseVm
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}