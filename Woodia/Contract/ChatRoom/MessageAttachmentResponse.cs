using Woodia.Enums;

namespace Woodia.Contract.ChatRoom;

public record MessageAttachmentResponse(
    int Id,
    string FileUrl,
    MessageType FileType);

