using System.Text;
using System.Text.Json;
using to_do.Models;

namespace to_do.Services;

public class ApiService
{
    private const string BaseUrl = "https://todo-list.dcism.org";
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<SignInResponse?> SignInAsync(string email, string password)
    {
        try
        {
            string url =
                $"{BaseUrl}/signin_action.php?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}";

            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SignInResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            return new SignInResponse
            {
                Status = 500,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public async Task<BasicResponse?> SignUpAsync(SignUpRequest request)
    {
        try
        {
            string url = $"{BaseUrl}/signup_action.php";

            var jsonBody = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<BasicResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            return new BasicResponse
            {
                Status = 500,
                Message = $"Error: {ex.Message}"
            };
        }
    }
    
    public async Task<GetTasksResponse?> GetItemsAsync(string status, int userId)
    {
        try
        {
            string url = $"{BaseUrl}/getItems_action.php?status={Uri.EscapeDataString(status)}&user_id={userId}";

            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetTasksResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            return new GetTasksResponse
            {
                Status = 500,
                Count = "0"
            };
        }
    }

    public async Task<AddTaskResponse?> AddItemAsync(AddTaskRequest request)
    {
        try
        {
            string url = $"{BaseUrl}/addItem_action.php";

            var jsonBody = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<AddTaskResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            return new AddTaskResponse
            {
                Status = 500,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public async Task<DeleteTaskResponse?> DeleteItemAsync(int itemId)
    {
        try
        {
            string url = $"{BaseUrl}/deleteItem_action.php?item_id={itemId}";

            var response = await _httpClient.DeleteAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DeleteTaskResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            return new DeleteTaskResponse
            {
                Status = 500,
                Message = $"Error: {ex.Message}"
            };
        }
    }
}