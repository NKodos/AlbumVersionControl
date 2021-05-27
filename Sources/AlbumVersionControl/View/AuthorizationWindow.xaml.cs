using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls.Primitives;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Extensions;
using AlbumVersionControl.Models.GitHubApi;
using GitApi.Interfaces;

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
            ShowCurrentConnection();
        }

        private void ShowCurrentConnection()
        {
            var connection = GetConnectionFromConfig();
            LocalUserNameTextBox.Text = connection.Login;
            LocalPasswordBox.Password = connection.Password;
            TokenTextBox.Text = connection.Token;
        }

        private static GitHubConnectionElement GetConnectionFromConfig()
        {
            var section = (GitHubConnectionConfigSection)ConfigurationManager.GetSection("GitHubConnection");
            var connectionFromConfig = section.GitHubConnectionItems?[0];
            return connectionFromConfig;
        }

        private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connection = GetGitHubConnection();
                Program.GitHubService.Connect(connection);
                SaveSettings(connection);
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

        private static void SaveSettings(IGitConnection connection)
        {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = (GitHubConnectionConfigSection)cfg.Sections["GitHubConnection"];
            var connectionFromConfig = section.GitHubConnectionItems?[0];
            if (connectionFromConfig != null)
            {
                System.Diagnostics.Debug.WriteLine(connectionFromConfig.Login);
                connectionFromConfig.Login = connection.Login;
                System.Diagnostics.Debug.WriteLine(connectionFromConfig.Password);
                connectionFromConfig.Password = connection.Password;
                System.Diagnostics.Debug.WriteLine(connectionFromConfig.Token);
                connectionFromConfig.Token = connection.Token;
                cfg.Save();
                ConfigurationManager.RefreshSection("GitHubConnection");
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