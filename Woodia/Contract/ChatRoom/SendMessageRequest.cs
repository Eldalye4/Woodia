namespace Woodia.Contract.ChatRoom;

public record SendMessageRequest(
    int RoomId,
    string? Content,
    List<AttachmentRequest>? Attachments);