using System;
using to_do.Services;

namespace to_do;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(EmailEntry.Text) &&
            !string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            var response = await _apiService.SignInAsync(EmailEntry.Text, PasswordEntry.Text);

            if (response?.Status == 200)
            {
                AuthState.Username = EmailEntry.Text;
                AuthState.UserId = response.Data.Id;

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync(nameof(TodoPage));
            }
            else
            {
                await DisplayAlert("Error", response?.Message ?? "Sign in failed.", "OK");
            }
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