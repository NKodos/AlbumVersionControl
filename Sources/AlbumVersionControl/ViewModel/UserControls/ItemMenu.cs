using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace AlbumVersionControl.ViewModel.UserControls
{
    public class ItemMenu : UserControl
    {
        public ItemMenu(string header, List<SubItem> subItems, PackIconKind icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
            HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        public ItemMenu(string header, UserControl screen, PackIconKind icon)
        {
            Header = header;
            Screen = screen;
            Icon = icon;
        }

        public string Header { get; }
        public PackIconKind Icon { get; }
        public List<SubItem> SubItems { get; }
        public UserControl Screen { get; }
    }
}