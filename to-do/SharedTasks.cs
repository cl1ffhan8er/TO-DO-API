using System.Collections.ObjectModel;
using to_do.Models;

namespace to_do
{
    public static class SharedTasks
    {
        public static ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();
        public static ObservableCollection<TaskItem> CompletedTasks { get; } = new ObservableCollection<TaskItem>();
    }
}