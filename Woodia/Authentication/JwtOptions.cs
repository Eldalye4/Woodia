public class JwtOptions
{
    public static string SectionName = "jwt";
    public string key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiryMintes { get; set; }
}

