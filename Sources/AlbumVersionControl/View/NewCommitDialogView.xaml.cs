using System.Windows;

namespace AlbumVersionControl.View
{
    /// <summary>
    /// Interaction logic for NewCommitDialogView.xaml
    /// </summary>
    public partial class NewCommitDialogView : Window
    {
        public NewCommitDialogView()
        {
            InitializeComponent();
        }

        public string Message
        {
            get => txtMessage.Text;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
