using System.Windows;

namespace AlbumVersionControl.View
{
    /// <summary>
    ///     Interaction logic for NewProjcetDialogView.xaml
    /// </summary>
    public partial class NewProjcetDialogView : Window
    {
        public NewProjcetDialogView()
        {
            InitializeComponent();
        }

        public string ProjectName
        {
            get => txtName.Text;
        }

        public string ProjectDescription
        {
            get => txtDescription.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}