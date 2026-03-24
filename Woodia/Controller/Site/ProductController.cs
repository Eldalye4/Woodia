using Microsoft.AspNetCore.Mvc;
using Woodia.Service.Interface;

namespace Woodia.Controller.Site;


[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "client")]
public class ProductController: ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService  productService)
    {
        _productService = productService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery]RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var result= await _productService.GetAllAsync(requestFilters,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var result=await _productService.GetPublicGyId(Id,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("most-loved")]
    public async Task<IActionResult> GetMostLoved(CancellationToken cancellationToken)
    {
        var result = await _productService.GetMostLovedAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}