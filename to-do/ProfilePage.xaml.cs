using System.Collections.ObjectModel;
using to_do.Models;

namespace to_do;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UsernameLabel.Text = AuthState.Username ?? "";
    }

    void SignOutClicked(object sender, EventArgs e)
    {
        AuthState.SignOut();
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}

