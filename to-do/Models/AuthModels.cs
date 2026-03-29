using System.Text.Json.Serialization;

namespace to_do.Models;

public class SignInResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("data")]
    public UserData? Data { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class UserData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("fname")]
    public string? FName { get; set; }

    [JsonPropertyName("lname")]
    public string? LName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("timemodified")]
    public string? TimeModified { get; set; }
}

public class SignUpRequest
{
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("confirm_password")]
    public string? ConfirmPassword { get; set; }
}

public class BasicResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}