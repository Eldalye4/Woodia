using Mapster;
using Microsoft.EntityFrameworkCore;
using Woodia.Contract.ChatRoom;
using Woodia.Entity;
using Woodia.Enums;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class ChatService:IChatService
{
    private readonly ApplicationDbContext _context;
    public ChatService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<ChatRoomResponse>> NegotiateOffer(string userId, int offerId,CancellationToken cancellationToken )
    {
        var chatRoom = await _context.ChatRooms.FirstOrDefaultAsync(x => x.OfferId == offerId, cancellationToken);
        if (chatRoom != null)
        {
            if (chatRoom.ClientId != userId && chatRoom.CarpenterId != userId)
                return Result.failure<ChatRoomResponse>(ChatError.AccessDenied);
            return Result.Success(chatRoom.Adapt<ChatRoomResponse>());
        }
            
        
        if (await _context.Offers.Include(x=>x.Job).FirstOrDefaultAsync(x => x.Id == offerId, cancellationToken) is not { } offer)
        {
            return Result.failure<ChatRoomResponse>(OfferError.OfferNotFound);
        }

        if (offer.Job.ClientId != userId)
        {
            return Result.failure<ChatRoomResponse>(OfferError.NotJobOwner);
        }

        var NewchatRoom = new ChatRoom
        {
            OfferId = offerId,
            ClientId = userId,
            CarpenterId = offer.CarpenterId,

        };
        await _context.ChatRooms.AddAsync(NewchatRoom ,cancellationToken);

        var newMessage = new Message
        {
            ChatRoom = NewchatRoom,
            SenderId = null,
            Content = "The client has started a chat to discuss the offer.",
            MessageType = MessageType.System
        };
        await _context.Messages.AddAsync(newMessage ,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(NewchatRoom.Adapt<ChatRoomResponse>());
    }
    
    public async Task<Result<PaginatedList<ChatRoomListResponse>>> GetAllRoom(string userId,RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var Chats =  _context
            .ChatRooms
            .Where(x => x.ClientId == userId || x.CarpenterId == userId).OrderByDescending(x => x.LastMessageDate)
            .Select(x => new ChatRoomListResponse
            (
                x.Id,
                x.CarpenterId == userId ? x.ClientId : x.CarpenterId,
                x.CarpenterId == userId
                    ? $"{x.Client.FirstName} {x.Client.LastName}"
                    : $"{x.Carpenter.FirstName} {x.Carpenter.LastName}"
                ,
                x.Offer.Job.Title, 
                x.Messages.OrderByDescending(y=>y.SentAt).Select(s=>s.Content).FirstOrDefault()??""
                , x.LastMessageDate,
                x.Messages.Count(y => y.SenderId != userId && !y.IsRead)


            ));
        var response =
            await PaginatedList<ChatRoomListResponse>
                .CreateAsync(Chats, requestFilters.PageNumber, requestFilters.PageSize,cancellationToken);
        return Result.Success(response);
    }
    
    public async Task<Result> CanAccessChatAsync(string userId,int roomId)
    {
        if (await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == roomId) is not { } room)
        {
            return Result.failure(ChatError.ChatNotFound);
        }

        if (room.ClientId != userId && room.CarpenterId != userId)
        {
            return Result.failure(ChatError.AccessDenied);
        }
        
        
        return Result.Success();
    }
    
    public async Task<Result<MessageResponse>> SaveMessage(string userId,SendMessageRequest  sendMessageRequest)
    {
        if (!await _context.ChatRooms.AnyAsync(x => x.Id == sendMessageRequest.RoomId &&(x.ClientId == userId || x.CarpenterId == userId)))
        {
            return Result.failure<MessageResponse>(ChatError.ChatNotFound);
        }

        var messageType = (sendMessageRequest.Content, sendMessageRequest.Attachments) switch
        {
            ( _ , { Count: > 0 }) => sendMessageRequest.Attachments[^1].FileType,
            _ => MessageType.Text
        };
        var NewMessage = new Message
        {
            ChatRoomId = sendMessageRequest.RoomId,
            SenderId = userId,
            Content = sendMessageRequest.Content??string.Empty,
            IsRead = false,
            MessageType =messageType,
            Attachments = sendMessageRequest.Attachments?.Select(a=>
                new MessageAttachment
                {
                    FileUrl = a.FileUrl,
                    FileType = a.FileType
                }).ToList()??new()
        };
        await _context.Messages.AddAsync( NewMessage);
        await _context.SaveChangesAsync();
        return Result.Success(NewMessage.Adapt<MessageResponse>());
    }
    public async Task<Result<PaginatedList<MessageResponse>>> GetMessage(string userId,RequestFilters requestFilters,int RoomId,CancellationToken cancellationToken )
    {
        if (!await _context.ChatRooms.AnyAsync(x => (x.ClientId == userId || x.CarpenterId == userId) && x.Id == RoomId, cancellationToken))
        {
            return Result.failure<PaginatedList<MessageResponse>>(ChatError.ChatNotFound);
        }
        await _context.Messages
            .Where(x=>x.ChatRoomId==RoomId && !x.IsRead && x.SenderId != userId)
            .ExecuteUpdateAsync(x=>x.SetProperty(y=>y.IsRead,true),cancellationToken);

        var message = _context.Messages.Where(x => x.ChatRoomId == RoomId)
            .OrderByDescending(x=>x.SentAt)
            .ProjectToType<MessageResponse>();
        
        var response =
            await PaginatedList<MessageResponse>
                .CreateAsync(message, requestFilters.PageNumber, requestFilters.PageSize,cancellationToken);
        
        return Result.Success(response);

    }
}