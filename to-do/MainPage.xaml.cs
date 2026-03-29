using System;

namespace to_do;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(EmailEntry.Text) &&
            !string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            AuthState.Username = EmailEntry.Text;
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync(nameof(TodoPage));
        }
        else
        {
            await DisplayAlert("Error", "Please fill in email and password.", "OK");
        }
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}