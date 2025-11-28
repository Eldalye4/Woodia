using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "client")]
public class GovernorateController : ControllerBase
{
    private readonly IGovernorateService _governorateService;

    public GovernorateController(IGovernorateService governorateService)
    {
        _governorateService= governorateService;
    }
    [HttpGet("")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok(await _governorateService.GetAllGovernorate(cancellationToken));
    } 
}