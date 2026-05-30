using Woodia.Enums;

namespace Woodia.Entity;

public class MessageAttachment
{
    public int Id{get;set;}
    public int MessageId{get;set;}
    public string FileUrl{get;set;}=string.Empty;
    public MessageType FileType {get;set;}
    public Message Message{get;set;}
}