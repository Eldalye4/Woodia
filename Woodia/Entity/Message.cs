using Woodia.Enums;

namespace Woodia.Entity;

public class Message
{
    public int Id{get;set;}
    public int ChatRoomId{get;set;}
    public string? SenderId{get;set;}
    public string Content{get;set;}=string.Empty;
    public DateTime SentAt{get;set;}=DateTime.UtcNow;
    public bool IsRead{get;set;}
    public MessageType MessageType{get;set;}
    public ApplicationUser Sender{get;set;}
    public ChatRoom ChatRoom{get;set;}
    public List<MessageAttachment> Attachments { get; set; } = new();
}