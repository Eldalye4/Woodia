using Microsoft.AspNetCore.Identity;
namespace Woodia.Entity;
public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserType userType { get; set; }
    public string? PhotoUrl { get; set; }

    public List<Otp> otps { get; set; } = [];
    public List<RefreshToken> RefreshTokens { get; set; } = [];

    public ContactInformation contactInformation{ get; set; } =null!;
    public CarpenterProfile? CarpenterProfile { get; set; } 
    public List<Job>? Job{ get; set; }
    public List<Offer> Offers { get; set; } = [];
    public List<ChatRoom> ClientChatRooms{get;set;} = [];
    public List<ChatRoom> CarpenterChatRooms{get;set;} = [];
}