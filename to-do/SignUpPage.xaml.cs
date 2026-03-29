using System;
using System.Text;
using System.Text.Json;

namespace to_do;

public partial class SignUpPage : ContentPage
{
    private const string BaseUrl = "https://todo-list.dcism.org";

    public SignUpPage()
    {
        InitializeComponent();
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill all fields.", "OK");
            return;
        }

        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        try
        {
            using var client = new HttpClient();

            var signupData = new
            {
                first_name = FirstNameEntry.Text.Trim(),
                last_name = LastNameEntry.Text.Trim(),
                email = EmailEntry.Text.Trim(),
                password = PasswordEntry.Text,
                confirm_password = ConfirmPasswordEntry.Text
            };

            var json = JsonSerializer.Serialize(signupData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{BaseUrl}/signup_action.php", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseBody);
            int status = doc.RootElement.GetProperty("status").GetInt32();
            string message = doc.RootElement.GetProperty("message").GetString() ?? "Unknown response.";

            if (status == 200)
            {
                await DisplayAlert("Success", message, "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Sign Up Failed", message, "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Something went wrong: {ex.Message}", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}