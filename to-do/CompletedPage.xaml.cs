using System.Collections.ObjectModel;
using to_do.Models;
using to_do.Services;

namespace to_do;

public partial class CompletedPage : ContentPage
{
    private readonly ApiService _apiService = new();
    public CompletedPage()
    {
        InitializeComponent();
        completedList.ItemsSource = SharedTasks.CompletedTasks;
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is TaskItem task)
        {
            var response = await _apiService.DeleteItemAsync(task.ItemId);

            if (response?.Status == 200)
            {
                SharedTasks.CompletedTasks.Remove(task);
            }
            else
            {
                await DisplayAlert("Error", response?.Message ?? "Failed to delete task.", "OK");
            }
        }
    }

    async void OnUncompleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is TaskItem task)
        {
            var request = new ChangeStatusRequest
            {
                ItemId = task.ItemId,
                Status = "active"
            };

            var response = await _apiService.ChangeStatusAsync(request);

            if (response?.Status == 200)
            {
                SharedTasks.CompletedTasks.Remove(task);

                if (!SharedTasks.Tasks.Contains(task))
                    SharedTasks.Tasks.Add(task);
            }
            else
            {
                await DisplayAlert("Error", response?.Message ?? "Failed to update status.", "OK");
            }
        }
    }
}

