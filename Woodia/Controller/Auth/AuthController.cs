using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuth _auth;

    public AuthController(IAuth auth)
    {
        _auth = auth;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var result = await _auth.Register(registerRequest, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest confirmEmail, CancellationToken cancellationToken)
    {
        var result = await _auth.ConfirmEmail(confirmEmail, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("")]
    public async Task<IActionResult> Login([FromBody] AuthRequest authRequest, CancellationToken cancellationToken)
    {
        var result = await _auth.GetToken(authRequest.Email, authRequest.Password, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await _auth.GetRefreshToken(request.Token, request.RefreshToken, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}