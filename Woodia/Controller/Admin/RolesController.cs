using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Role;
using Woodia.Contract.Upload;
using Woodia.Service.Interface;

namespace Woodia.Controller.Admin;
[Route("admin/[controller]")]
[ApiExplorerSettings(GroupName = "admin")]
[ApiController]
public class RolesController:ControllerBase
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled,CancellationToken cancellationToken)
    {
        var responses = await _roleService.GetAllAsync(cancellationToken,includeDisabled);
        return Ok(responses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var result=await _roleService.GetAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] RoleRequest request)
    {
        var result=await _roleService.AddAsync(request);
        return result.IsSuccess ? CreatedAtAction(nameof(Get),new{result.Value.Id},result.Value) : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id,[FromBody] RoleRequest request)
    {
        var result=await _roleService.UpdateAsync(id, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPatch("{id}/toggle-status")]
    public async Task<IActionResult> ToggleStatus([FromRoute] string id)
    {
        var result = await _roleService.ToggleStatusAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}