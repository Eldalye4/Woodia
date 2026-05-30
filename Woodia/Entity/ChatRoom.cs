namespace Woodia.Entity;

public class ChatRoom
{
    public int Id{get;set;}
    public int OfferId { get; set; }
    public string ClientId{get;set;}=null !;
    public string CarpenterId{get;set;}= null !;
    public DateTime LastMessageDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
    public ApplicationUser Client{get;set;}
    public ApplicationUser Carpenter{get;set;}
    public Offer Offer{get;set;} 
    public List<Message> Messages { get; set; } = new();
}