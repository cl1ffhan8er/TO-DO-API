using System.Collections.ObjectModel;
using to_do.Models;
using to_do.Services;

namespace to_do;

public partial class TodoPage : ContentPage
{
    private readonly ApiService _apiService = new();

    public TodoPage()
    {
        InitializeComponent();
        taskList.ItemsSource = SharedTasks.Tasks;
    }

    async void OnAddClicked(object sender, EventArgs e)
    {
        string title = await DisplayPromptAsync("New Task", "Enter Title");
        string details = await DisplayPromptAsync("Task Details", "Enter Details");

        if (!string.IsNullOrEmpty(title))
        {
            var request = new AddTaskRequest
            {
                ItemName = title,
                ItemDescription = details,
                UserId = AuthState.UserId
            };

            var response = await _apiService.AddItemAsync(request);

            if (response?.Status == 200)
            {
                SharedTasks.Tasks.Add(response.Data);
            }
            else
            {
                await DisplayAlert("Error", response?.Message ?? "Failed to add task.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Task is not specified.", "OK");
        }
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        TaskItem task = btn.BindingContext as TaskItem;

        var response = await _apiService.DeleteItemAsync(task.ItemId);

        if (response?.Status == 200)
        {
            SharedTasks.Tasks.Remove(task);
        }
        else
        {
            await DisplayAlert("Error", response?.Message ?? "Failed to delete task.", "OK");
        }
    }

    async void OnEditClicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        TaskItem task = btn.BindingContext as TaskItem;

        string newTitle = await DisplayPromptAsync("Edit Title", "", initialValue: task.ItemName);
        string newDetails = await DisplayPromptAsync("Edit Details", "", initialValue: task.ItemDescription);

        if (!string.IsNullOrEmpty(newTitle))
        {
            task.ItemName = newTitle;
            task.ItemDescription = newDetails;

            taskList.ItemsSource = null;
            taskList.ItemsSource = SharedTasks.Tasks;
        }
    }

    void OnCompletedClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is TaskItem task)
        {
            if (SharedTasks.Tasks.Contains(task))
                SharedTasks.Tasks.Remove(task);

            if (!SharedTasks.CompletedTasks.Contains(task))
                SharedTasks.CompletedTasks.Add(task);
        }
    }
}