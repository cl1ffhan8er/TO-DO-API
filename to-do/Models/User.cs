using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Postgrest.Attributes;
using Postgrest.Models;

namespace to_do.Models;

[Table("users")]
public class User: BaseModel, INotifyPropertyChanged
{
    [SetsRequiredMembers]
    public User() { }
    
    private int _id;
    private string _username;
    private string _email;
    private string _password;
    
    [PrimaryKey("id", false)]
    public int Id { get => _id; set; }
    
    [Column("username")]
    public required string Username { get => _username; set { _username = value; OnPropertyChanged(); }  }
    
    [Column("email_address")]
    public required string Email { get => _email; set { _email = value; OnPropertyChanged(); }  }
    
    [Column("password")]
    public required string Password { get => _password; set {  _password = value; OnPropertyChanged(); }  }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}