using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentNotesViewer;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnTextInputChanged(object sender, TextChangedEventArgs e)
    {
        LoginButton.IsEnabled = new[] { UsernameTextBox, PasswordTextBox }.All(x => x is { Text.Length: > 0 });
    }

    private void LogIn(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text;
        var password = PasswordTextBox.Text;
        var student = MainWindow.Students.Find(x => x.Name.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password == password);

        if (student == null)
        {
            MessageBox.Show("The specified username and password combination does not exist. Please check for typos and try again.", "Invalid username or password", MessageBoxButton.OK, MessageBoxImage.Error);
            LoginButton.IsEnabled = false;
            return;
        }

        MainWindow.Instance.Frame.NavigationService.Navigate(new OverviewPage(student));
    }

    private void TextBoxMouseEnter(object sender, MouseEventArgs e)
    {
        ((TextBox)sender).Focus();
    }
}