using Woodia.Enums;

namespace Woodia.Contract.ChatRoom;

public record ChatRoomListResponse(
    int RoomId,
    string UserId,
    string Name,
    string JobTitle,
    string lastMessageContent,
    DateTime lastMessageDate,
    MessageType MessageType,
    int UnreadMessageCount
    );