using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Postgrest.Attributes;
using Postgrest.Models;

namespace to_do.Models;

[Table("tasks")]
public class Task: BaseModel, INotifyPropertyChanged
{
    [SetsRequiredMembers]
    public Task() { }
    
    private int _id;
    private string _name;
    private string _details;
    private string _status;
    private int _userId;

    [PrimaryKey("id", false)]
    public int Id 
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); }
    }
    
    [Column("name")]
    public string Name 
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }
    
    [Column("details")]
    public string Details 
    {
        get => _details;
        set { _details = value; OnPropertyChanged(); }
    }
    
    [Column("status")]
    public string Status 
    {
        get => _status;
        set { _status = value; OnPropertyChanged(); }
    }
    
    [Column("user")]
    public int UserId 
    {
        get => _userId;
        set { _userId = value; OnPropertyChanged(); }
    }
    
    [Reference(typeof(User))]
    public User AssociatedUser { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}