using System.Collections.ObjectModel;
using to_do.Models;

namespace to_do;

public partial class CompletedPage : ContentPage
{
    public CompletedPage()
    {
        InitializeComponent();
        completedList.ItemsSource = SharedTasks.CompletedTasks;
    }       

    void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is TaskItem task)
        {
            SharedTasks.CompletedTasks.Remove(task);
        }
    }

    void OnUncompleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is TaskItem task)
        {
            if (SharedTasks.CompletedTasks.Contains(task))
                SharedTasks.CompletedTasks.Remove(task);

            if (!SharedTasks.Tasks.Contains(task))
                SharedTasks.Tasks.Add(task);
        }
    }
}

