using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Service.Interface;

namespace Woodia.Controller.Carpenter;
[Route("api/carpenter/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "carpenter")]
[Authorize(Roles = "Carpenter")]
public class JobController:ControllerBase
{
    private readonly IJobService _jobService;
    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }
    [HttpGet("available")]
    public async Task<IActionResult> GetAll([FromQuery]RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var result = await _jobService.GetAllForCarpenterAsync(requestFilters, cancellationToken);
        return result.IsSuccess? Ok(result.Value) : BadRequest(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetAsync(int Id, CancellationToken cancellationToken)
    {
        var result=await _jobService.GetForCarpenterAsync(Id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}