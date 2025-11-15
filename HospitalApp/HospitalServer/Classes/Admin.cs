using System.ComponentModel.DataAnnotations;

public class Admin
{
    public int AdminID { get; set; } // FK to User.UserID
    public string? Access_Level { get; set; } // 'Low', 'Medium', 'High'

    public User? User { get; set; }
}