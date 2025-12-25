using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "client")]
public class CategoryController : ControllerBase
{
    private readonly ICategory _category;

    public CategoryController(ICategory category)
    {
        _category = category;
    }
    [HttpGet("")]
    [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _category.GetAll(cancellationToken));
    }
    [HttpGet("{Slug}")]
    [ProducesResponseType(typeof(CategoryNavigationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNavigation([FromRoute] string Slug, CancellationToken cancellationToken)
    {
        var result = await _category.GetNavigation(Slug, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("Parent")]
    [ProducesResponseType(typeof(CategoryParentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetParent(CancellationToken cancellationToken)
    {
        return Ok(await _category.GetParent(cancellationToken));
    }
}