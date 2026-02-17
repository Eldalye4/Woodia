using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Carpenter;
using Woodia.Service.Interface;

namespace Woodia.Controller.Carpenter;
[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Carpenter")]
[ApiExplorerSettings(GroupName = "carpenter")]
public class CarpenterController:ControllerBase
{
    private readonly ICarpenterService _carpenterService;
    public CarpenterController(ICarpenterService carpenterService)
    {
        _carpenterService = carpenterService;
    }
    [HttpPost("professional-profile")]
    public async Task<IActionResult> AddProfessionalProfile(ProfessionalProfileRequest professionalProfileRequest)
    {
        var result=await _carpenterService.AddProfessionalProfile(User.GetUserById()!, professionalProfileRequest);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("professional-profile")]
    public async Task<IActionResult> EditProfessionalProfile(ProfessionalProfileRequest professionalProfileRequest)
    {
        var result = await _carpenterService.UpdateProfessionalProfile(User.GetUserById()!, professionalProfileRequest);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet("professional-profile")]
    public async Task<IActionResult> GetProfessionalProfile()
    {
        var result = await _carpenterService.GetProfessionalProfile(User.GetUserById()!);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("Portfolio-Item")]
    public async Task<IActionResult> AddPortfolioItem(PortfolioItemRequest portfolioItemRequest,CancellationToken cancellationToken )
    {   
        var result=await _carpenterService.AddPortfolioItem(User.GetUserById()!,portfolioItemRequest,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("Portfolio-Item/{Id}")]
    public async Task<IActionResult> GetPortfolioItem([FromRoute]int Id,CancellationToken cancellationToken )
    {
        var result = await _carpenterService.GetPortfolioItem(Id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(); 
    }
    [HttpPut("Portfolio-Item/{Id}")]
    public async Task<IActionResult> UpdatePortfolioItem([FromRoute]int Id,PortfolioItemRequest portfolioItemRequest,CancellationToken cancellationToken )
    {
        var result = await _carpenterService.UpdatePortfolioItem(Id,portfolioItemRequest,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem(); 
    }

    [HttpDelete("Portfolio-Item/{Id}")]
    public async Task<IActionResult> DeletePortfolioItem([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var result=await _carpenterService.DeletePortfolioItem(Id, cancellationToken);
        return result.IsSuccess ? NoContent()  : result.ToProblem();
    }

    [HttpGet("Portfolio-Items")]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilters requestFilters,CancellationToken cancellationToken )
    {
        var result=await _carpenterService.GetAllAsync(User.GetUserById()!,requestFilters, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}