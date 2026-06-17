using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Woodia.Contract.Category;
using Woodia.Contract.Product;
using Woodia.Contract.Upload;
using Woodia.Service.Interface;

namespace Woodia.Controller.Admin;
[Route("api/admin/[controller]")]
[ApiExplorerSettings(GroupName = "admin")]
[ApiController]
[Authorize(Roles = "SuperAdmin")]
public class CategoryController:ControllerBase
{
    private readonly ICategory _category;
    public CategoryController(ICategory Icategory  )
    {
        _category = Icategory;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(PaginatedList<AdminCategoryListingResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var result = await _category.GetAllForAdminAsync(requestFilters, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AdminCategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id,CancellationToken cancellationToken)
    {
        var result=await _category.GetForAdminAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [ProducesResponseType(typeof(List<CategoryListingForAdminResponse>), StatusCodes.Status200OK)]
    [HttpGet("parents")]
    public async Task<IActionResult> GetParents(CancellationToken cancellationToken)
    {
        var result=await _category.GetParentsForAdminAsync(cancellationToken);
        return result.IsSuccess? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("childrens")]
    public async Task<IActionResult> GetChildrens(CancellationToken cancellationToken)
    {
        var result=await _category.GetChild(cancellationToken);
        return result.IsSuccess? Ok(result.Value) : result.ToProblem();
    }
    [ProducesResponseType(typeof(AdminCategoryResponse), StatusCodes.Status200OK)]
    [HttpPost("")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequest categoryRequest,
        CancellationToken cancellationToken)
    {
        var result =await _category.AddForAdminAsync(categoryRequest, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result=await _category.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryRequest categoryRequest,
        CancellationToken cancellationToken)
    {
        var result = await _category.UpdateAsync(id, categoryRequest, cancellationToken);
        return result.IsSuccess ? NoContent(): result.ToProblem();
    }
}