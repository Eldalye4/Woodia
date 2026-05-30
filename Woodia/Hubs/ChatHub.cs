using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Woodia.Contract.ChatRoom;
using Woodia.Service.Interface;

namespace Woodia.Hubs;
[Authorize]
public class ChatHub:Hub
{
    private readonly IChatService _chatService;
    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }
    public async Task JoinGroup(int roomId)
    {
        var userId = Context.UserIdentifier;
        
        var result=await _chatService.CanAccessChatAsync(userId,roomId);
        if (!result.IsSuccess)
        {
            throw new HubException(result.Error.Description);
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
    }
    public async Task SendMessage(SendMessageRequest sendMessageRequest, int roomId)
    {
        var userId = Context.UserIdentifier;
        var result=await _chatService.SaveMessage(userId,sendMessageRequest);
        if (!result.IsSuccess)
        {
            throw new HubException(result.Error.Description);
        }
        await Clients.OthersInGroup(roomId.ToString()).SendAsync("ReceiveMessage",result.Value);
    }
    
}