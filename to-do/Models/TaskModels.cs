using System.Text.Json.Serialization;

namespace to_do.Models;

public class TaskItem
{
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; }

    [JsonPropertyName("item_name")]
    public string ItemName { get; set; } = string.Empty;

    [JsonPropertyName("item_description")]
    public string ItemDescription { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("time_modified")]
    public string TimeModified { get; set; } = string.Empty;
}

public class AddTaskRequest
{
    [JsonPropertyName("item_name")]
    public string ItemName { get; set; } = string.Empty;

    [JsonPropertyName("item_description")]
    public string ItemDescription { get; set; } = string.Empty;

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
}

public class AddTaskResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("data")]
    public TaskItem? Data { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public class GetTasksResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<string, TaskItem>? Data { get; set; }

    [JsonPropertyName("count")]
    public string Count { get; set; } = string.Empty;

    public List<TaskItem> Items => Data?.Values.ToList() ?? new List<TaskItem>();
}

public class DeleteTaskResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public class UpdateTaskRequest
{
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; }

    [JsonPropertyName("item_name")]
    public string? ItemName { get; set; }

    [JsonPropertyName("item_description")]
    public string? ItemDescription { get; set; }
}

public class ChangeStatusRequest
{
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}