using System.Collections.ObjectModel;
using to_do.Models;

namespace to_do;

public partial class TodoPage : ContentPage
{
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
            SharedTasks.Tasks.Add(new TaskItem
            {
                Title = title,
                Details = details
            });
        }
        else
        {
            await DisplayAlert("Error", "Task is not specified.", "OK");
        }
    }

    void OnDeleteClicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        TaskItem task = btn.BindingContext as TaskItem;
        SharedTasks.Tasks.Remove(task);
    }

    async void OnEditClicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        TaskItem task = btn.BindingContext as TaskItem;

        string newTitle = await DisplayPromptAsync("Edit Title", "", initialValue: task.Title);
        string newDetails = await DisplayPromptAsync("Edit Details", "", initialValue: task.Details);

        task.Title = newTitle;
        task.Details = newDetails;

        taskList.ItemsSource = null;
        taskList.ItemsSource = SharedTasks.Tasks;
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

