using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Offer;
using Woodia.Service.Interface;

namespace Woodia.Controller.Carpenter;
[Route("api/carpenter/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "carpenter")]
public class OffersController:ControllerBase
{
    private readonly IOfferService _offerService ;
    public OffersController(IOfferService offerService)
    {
        _offerService = offerService;
    }
    [HttpPost("~/api/carpenter/jobs/{jobId}/offers")]
    public async Task<IActionResult> Add([FromRoute] int jobId,OfferRequest offerRequest)
    {
        var result = await _offerService.AddAsync(User.GetUserById()!, jobId, offerRequest);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var result =await _offerService.GetAllAsync(requestFilters, User.GetUserById()!, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result=await _offerService.GetAsync(User.GetUserById()!,id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPatch("{id}/withdraw")]
    public async Task<IActionResult> WithdrawOffer([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _offerService.withdrawStatusAsync(User.GetUserById()!, id, cancellationToken);
        return result.IsSuccess ? NoContent(): result.ToProblem();
    }
}