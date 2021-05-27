using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using AlbumVersionControl.Models.GitHubApi;

namespace AlbumVersionControl.View
{
    public partial class AuthorizationWindow : Window
    {
        private readonly string _errorMessage;

        public bool IsConected { get; set; }

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        public AuthorizationWindow(string errorMessage) : this()
        {
            _errorMessage = errorMessage;
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }

        private void btnActionClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowErrorMessage(_errorMessage);
        }

        private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connection = GetGitHubConnection();
                Program.GitHubService.Connect(connection);
                IsConected = true;
                ShowOkMessage("Подключение прошло успешно");
                HideErrorMessage();
            }
            catch (Exception ex)
            {
                IsConected = false;
                ShowErrorMessage("Не удалось подключиться");
                HideOkMessage();
            }
        }

        private GitHubConnection GetGitHubConnection()
        {
            if (TokenRadioButton.IsChecked != null && TokenRadioButton.IsChecked.Value)
            {
                return new GitHubConnection(TokenTextBox.Text);
            }
            if (LoginPasswordRadioButton.IsChecked != null && LoginPasswordRadioButton.IsChecked.Value)
            {
                return new GitHubConnection(LocalUserNameTextBox.Text, LocalPasswordBox.Password);
            }

            return new GitHubConnection{ Login = LocalUserNameTextBox.Text };
        }

        private void ShowErrorMessage(string errorMessage)
        {
            ErrorMessageTextBlock.Text = errorMessage;
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }

        private void HideErrorMessage()
        {
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }

        private void ShowOkMessage(string errorMessage)
        {
            OkMessageTextBlock.Text = errorMessage;
            OkMessageTextBlock.Visibility = Visibility.Visible;
        }

        private void HideOkMessage()
        {
            OkMessageTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}