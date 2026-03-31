using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.Upload;
using Woodia.Service.Interface;

namespace Woodia.Controller.Admin;
[Route("admin/[controller]")]
[ApiExplorerSettings(GroupName = "Upload")]
[ApiController]
[Authorize]
public class UploadController:ControllerBase
{
    private readonly Ifile _file;
    public UploadController(Ifile file)
    {
        _file = file;
    }
    [HttpPost("")]
    public async Task<IActionResult> Add([FromForm]UploadRequest uploadRequest, CancellationToken cancellationToken)
    {
        var urls = await _file.UploadManyFile(uploadRequest.Files, cancellationToken);
        return Ok(urls);
    }

    
}