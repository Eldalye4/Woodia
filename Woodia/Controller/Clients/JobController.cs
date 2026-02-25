using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Job;
using Woodia.Service.Interface;

namespace Woodia.Controller.Clients;

[Route("api/[controller]")]
[Authorize(Roles = "Client")]
[ApiController]
[ApiExplorerSettings(GroupName = "client")]
public class JobController:ControllerBase
{
    private readonly IJobService _jobService;
    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }
    [HttpPost("")]
    public async Task<IActionResult> Add(JobRequest request,CancellationToken cancellationToken)
    {
        var result=await _jobService.AddAsync(User.GetUserById()!, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery]RequestFilters requestFilters, CancellationToken cancellationToken)
    {
        var result = await _jobService.GetAllForClientAsync(requestFilters, User.GetUserById()!, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var result = await _jobService.GetForClientAsync(User.GetUserById()!,Id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update([FromRoute] int Id, JobRequest jobRequest,
        CancellationToken cancellationToken)
    {
        var result=await _jobService.UpdateAsync(User.GetUserById()!,Id, jobRequest, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPatch("{Id}/complete")]
    public async Task<IActionResult> CompleteState([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var result =await _jobService.CompleteStateAsync(User.GetUserById()!, Id, cancellationToken);
        return result.IsSuccess?NoContent():result.ToProblem();
    }
    [HttpPatch("{Id}/cancele")]
    public async Task<IActionResult> CanceleState([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var result =await _jobService.CanceleStateAsync(User.GetUserById()!, Id, cancellationToken);
        return result.IsSuccess?NoContent():result.ToProblem();
    }
    [HttpPatch("{Id}/inprogress")]
    public async Task<IActionResult> InProgressState([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var result =await _jobService.InProgressStateAsync(User.GetUserById()!, Id, cancellationToken);
        return result.IsSuccess?NoContent():result.ToProblem();
    }
    
    
}