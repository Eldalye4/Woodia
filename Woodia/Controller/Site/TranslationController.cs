using Microsoft.AspNetCore.Mvc;
using Woodia.Contract.MyMemory;
using Woodia.Service.Interface;

namespace Woodia.Controller.Site;
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "admin")]
public class TranslationController:ControllerBase
{
    private readonly ITranlationService _tranlationService;
    public TranslationController(ITranlationService tranlationService)
    {
        _tranlationService = tranlationService;
    }
    [HttpPost("translate")]
    public async Task<IActionResult> TestTranslation([FromBody] TranslationRequest translationRequest )
    {
        var translated = await _tranlationService.TranslateToEnglish(translationRequest);
        return Ok(new { original = translationRequest.Text, translated });
    }
}