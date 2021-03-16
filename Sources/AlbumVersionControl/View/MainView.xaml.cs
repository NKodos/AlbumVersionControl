using System.Collections.Generic;
using System.Windows.Controls;
using AlbumVersionControl.UserControls;
using AlbumVersionControl.UserControls.Objects;
using MaterialDesignThemes.Wpf;

namespace AlbumVersionControl.View
{
    /// <summary>
    ///     Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            InitializeNavigateMenu();
        }

        private void InitializeNavigateMenu()
        {
            var menuVersions = new List<SubItem>
            {
                new SubItem("V.1.0.0"), new SubItem("V.3.0.0"), new SubItem("V.4.0.0"), new SubItem("V.5.0.0")
            };


            AddNavigateItem(new ItemMenu("Проект №1", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Очень сложный проект", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Сложнейший проект со множеством всяких версий", menuVersions,
                PackIconKind.None));
            AddNavigateItem(new ItemMenu("Проект", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu(
                "Проект с длинным название, которое может занять очень много места, например название какого-то сложного приказа. " +
                "Тут может быть несколько предложений даже. Такой длинный текст, что я устал писать уже",
                menuVersions, PackIconKind.None));

            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Средний текст - копия", menuVersions, PackIconKind.None));
            AddNavigateItem(new ItemMenu("Настройки", new UserControl(), PackIconKind.Settings));
        }

        private void AddNavigateItem(ItemMenu itemMenu)
        {
            SideNavigateMenu.Children.Add(new UserControlMenuItem(itemMenu));
        }
    }
}