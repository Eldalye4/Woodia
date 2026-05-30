using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Service.Interface;

namespace Woodia.Controller.Site;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Chat")]
[Authorize(Roles =  "Client,Carpenter")]
public class ChatController:ControllerBase
{
    private readonly IChatService _chatService;
    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }
    [HttpGet("")]
    public async Task<IActionResult> GetRoom([FromRoute]RequestFilters requestFilters,CancellationToken cancellationToken )
    {
        var result=await _chatService.GetAllRoom(User.GetUserById()!,requestFilters,cancellationToken);
        return result.IsSuccess? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetMessage(int roomId,[FromQuery]RequestFilters requestFilters, CancellationToken cancellationToken)
    {
        var result = await _chatService.GetMessage(User.GetUserById()!, requestFilters, roomId, cancellationToken);
        return result.IsSuccess?Ok(result.Value) : result.ToProblem();
    }
}