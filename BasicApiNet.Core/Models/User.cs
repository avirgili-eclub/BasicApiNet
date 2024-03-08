namespace BasicApiNet.Core.Models;

public class User : BaseEntity
{
    //usernames are unique
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}