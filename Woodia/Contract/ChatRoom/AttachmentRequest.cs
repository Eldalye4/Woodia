using Woodia.Enums;

namespace Woodia.Contract.ChatRoom;

public record AttachmentRequest(
   string FileUrl,
   MessageType FileType);