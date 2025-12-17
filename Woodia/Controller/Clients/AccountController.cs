using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("me")]
[ApiController]
[ApiExplorerSettings(GroupName = "client")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IClientService clientService, ILogger<AccountController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }
    [HttpGet("")]
    public async Task<IActionResult> GetProfile()
    {
        var user = await _clientService.GetProfile(User.GetUserById()!);
        return Ok(user.Value);
    }
    [HttpPut("Info")]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileRequest profileRequest)
    {
        await _clientService.UpdateProfile(User.GetUserById()!, profileRequest);
        return NoContent();
    }
    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail([FromBody] EmailUpdateRequest request)
    {
        var result = await _clientService.UpadateEmail(User.GetUserById()!, request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("resend-email")]
    public async Task<IActionResult> ResendEmail([FromBody] EmailResendRequest emailResendRequest)
    {
        var result = await _clientService.ResendEmail(User.GetUserById()!, emailResendRequest.Email);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] ConfirmEmailRequest request)
    {
        var result = await _clientService.VerifyEmail(User.GetUserById()!, request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("contact-info")]

    public async Task<IActionResult> AddContactInformation([FromBody] ContactInformationRequest request, CancellationToken cancellationToken)
    {
        var result = await _clientService.AddContactInformation(User.GetUserById()!, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("contact-info")]
    public async Task<IActionResult> GetContactInformation(CancellationToken cancellationToken)
    {
        var result = await _clientService.GetContactInformation(User.GetUserById()!, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPut("contact-info")]
    public async Task<IActionResult> UpdateContactInformation([FromBody] ContactInformationRequest request, CancellationToken cancellationToken)
    {
        var result = await _clientService.UpdateContactInformation(User.GetUserById()!, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
