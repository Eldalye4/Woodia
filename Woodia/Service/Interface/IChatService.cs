using Woodia.Contract.ChatRoom;

namespace Woodia.Service.Interface;

public interface IChatService
{
    Task<Result<ChatRoomResponse>> NegotiateOffer(string userId, int offerId, CancellationToken cancellationToken);

    Task<Result<PaginatedList<ChatRoomListResponse>>> GetAllRoom(string userId, RequestFilters requestFilters,
        CancellationToken cancellationToken);

    Task<Result> CanAccessChatAsync(string userId, int roomId);

    Task<Result<MessageResponse>> SaveMessage(string userId, SendMessageRequest sendMessageRequest);

    Task<Result<PaginatedList<MessageResponse>>> GetMessage(string userId, RequestFilters requestFilters, int RoomId,
        CancellationToken cancellationToken);
}