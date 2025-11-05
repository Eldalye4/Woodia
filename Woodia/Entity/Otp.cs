using Microsoft.EntityFrameworkCore;

[Owned]
public class Otp
{
    public string Code { get; set; } = string.Empty;
    public DateTime ExpiresOn { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    public bool IsActive => !IsExpired;
}