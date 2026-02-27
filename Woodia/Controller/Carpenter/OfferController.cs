using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Offer;
using Woodia.Service.Interface;

namespace Woodia.Controller.Carpenter;
[Route("api/carpenter/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "carpenter")]
public class OfferController:ControllerBase
{
    private readonly IOfferService _offerService ;
    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }
    [HttpPost("{jobId}")]
    public async Task<IActionResult> Add([FromRoute] int jobId,OfferRequest offerRequest)
    {
        var result = await _offerService.AddAsync(User.GetUserById()!, jobId, offerRequest);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
 
}