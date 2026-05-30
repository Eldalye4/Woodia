using Woodia.Enums;

namespace Woodia.Entity;

public class Offer:AuditableEntity
{
    public int Id{get;set;}
    public decimal Price{get;set;}
    public string Description{get;set;}=string.Empty;
    public int DeliveryDay{get;set;}
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public int JobId{get;set;}
    public string CarpenterId{get;set;}
    public Job Job{get;set;}
    public ApplicationUser User{get;set;}
    public List<ChatRoom> ChatRooms{get;set;}
}