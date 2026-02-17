using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Product;
using Woodia.Service.Interface;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "client")]
public class CategoryController : ControllerBase
{
    private readonly ICategory _category;
    private readonly IProductService _productService;

    public CategoryController(ICategory category,IProductService  productService)
    {
        _category = category;
        _productService = productService;
    }
    [HttpGet("")]
    [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
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

    [HttpGet("{slug}/Products")]
    [ProducesResponseType(typeof(PaginatedList<ProductListingResponse>), StatusCodes.Status200OK)]
    
    public async Task<IActionResult> GetProductsByCategory([FromRoute] string slug,[FromQuery]RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var result =await _productService.GetByCategory(requestFilters, slug, cancellationToken);
        return result.IsSuccess ?Ok(result.Value) : result.ToProblem();

    }
}