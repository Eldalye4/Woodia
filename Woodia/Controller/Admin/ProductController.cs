using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Product;
using Woodia.Contract.Upload;
using Woodia.Entity;
using Woodia.Service.Interface;

namespace Woodia.Controller.Admin;
[Route("api/admin/[controller]")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Roles = "Admin")]
[ApiController]
public class ProductController:ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] ProductRequest productRequest, CancellationToken cancellationToken)
    {
        var result=await _productService.AddAsync(productRequest,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result=await _productService.GetAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [ProducesResponseType(typeof(PaginatedList<ProductGridItemResponse>), StatusCodes.Status200OK)]
    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilters requestFilters, CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllForAdminAsync(requestFilters, cancellationToken);
        return result.IsSuccess?  Ok(result.Value) : result.ToProblem();
        
    }
    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus(int id,[FromBody] ProductStatusRequest productStatusRequest,CancellationToken cancellationToken)
    {
        var result = await _productService.EditStatusForAdminAsync(id, productStatusRequest, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result=await _productService.DeleteForAdminAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateRequest productUpdateRequest,
        CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateForAdminAsync(id, productUpdateRequest, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    
}