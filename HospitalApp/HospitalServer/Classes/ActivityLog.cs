public class ActivityLog
{
    public int LogID { get; set; }
    public int? UserID { get; set; }
    public string? Action { get; set; }
    public DateTime Timestamp { get; set; }

    public User User { get; set; }
}