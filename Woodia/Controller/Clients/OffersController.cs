using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Service.Interface;

namespace Woodia.Controller.Clients;
[Route("api/jobs/")]
[Authorize(Roles = "Client")]
[ApiController]
[ApiExplorerSettings(GroupName = "client")]
public class OffersController:ControllerBase
{
    private readonly IOfferService _offerService;
    private readonly IChatService _chatService;
    private readonly ICarpenterService _carpenterService;
    public OffersController(IOfferService offerService, IChatService chatService, ICarpenterService carpenterService)
    {
        _offerService = offerService;
        _chatService = chatService;
        _carpenterService = carpenterService;
    }
    [HttpGet("/api/client/jobs/{jobId}/offers")]
    public async Task<IActionResult> GetOffers([FromQuery] RequestFilters requestFilters, [FromRoute] int jobId,
        CancellationToken cancellationToken)
    {
        var result=await _offerService.GetAllOfferByJobAsync(requestFilters,jobId,User.GetUserById()!,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("/api/client/jobs/{jobId}/offers/{offerId}")]
    public async Task<IActionResult> GetOffer(int jobId,int offerId,CancellationToken cancellationToken)
    {
        var result = await _offerService.GetOfferByJobAsync(jobId, User.GetUserById()!, offerId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("{offerId}/negotiate")]
    public async Task<IActionResult> negotiate(int offerId, CancellationToken cancellationToken = default)
    {
        var result=await _chatService.NegotiateOffer(User.GetUserById()!, offerId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("/api/carpenters/{carpenterId}/profile")]
    public async Task<IActionResult> GetCarpenterProfile(string carpenterId)
    {
        var result = await _carpenterService.GetCompleteProfile(carpenterId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    
    
}