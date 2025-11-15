using System.ComponentModel.DataAnnotations;

public class Device
{
    [Key]
    public int DeviceID { get; set; }
    public int UserID { get; set; }
    public string DeviceName { get; set; }
    public string Fingerprint { get; set; }
    public DateTime LastUsed { get; set; }

    public User User { get; set; }
}
