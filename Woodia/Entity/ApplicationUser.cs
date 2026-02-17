using Microsoft.AspNetCore.Identity;
namespace Woodia.Entity;
public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserType userType { get; set; }
    public List<Otp> otps { get; set; } = [];
    public List<RefreshToken> RefreshTokens { get; set; } = [];

    public ContactInformation contactInformation{ get; set; } =null!;
    public CarpenterProfile? CarpenterProfile { get; set; } 
}