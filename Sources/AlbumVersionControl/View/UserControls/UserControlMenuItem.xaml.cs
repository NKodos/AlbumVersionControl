using System.Windows;
using System.Windows.Controls;
using AlbumVersionControl.ViewModel.UserControls;

namespace AlbumVersionControl.View.UserControls
{
    /// <summary>
    ///     Interaction logic for UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        public UserControlMenuItem(ItemMenu itemMenu)
        {
            InitializeComponent();

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            DataContext = itemMenu;
        }
    }
}