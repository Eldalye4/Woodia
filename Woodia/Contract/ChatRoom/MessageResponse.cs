using Woodia.Entity;

namespace Woodia.Contract.ChatRoom;

public record MessageResponse(
    int Id,
    string? SenderId,
    string Content,
    DateTime SentAt,
    bool IsRead,
    string MessageType,
    List<MessageAttachmentResponse> Attachments);