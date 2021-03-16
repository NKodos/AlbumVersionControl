using System.Windows.Controls;

namespace AlbumVersionControl.ViewModel.UserControls
{
    public class SubItem
    {
        public SubItem(string name, UserControl screen = null)
        {
            Name = name;
            Screen = screen;
        }

        public string Name { get; }
        public UserControl Screen { get; }
    }
}