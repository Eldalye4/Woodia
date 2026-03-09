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
    public OffersController(IOfferService offerService)
    {
        _offerService = offerService;
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
}