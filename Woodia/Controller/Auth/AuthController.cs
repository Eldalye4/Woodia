using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "auth")]
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
    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendEmail([FromBody] ResendRequest request)
    {
        var result = await _auth.RensendEmail(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("google-Signup")]
    public async Task<IActionResult> SigninGoogle([FromBody] RegisterGoogleRequest authGoogleRequest, CancellationToken cancellationToken)
    {
        var result = await _auth.SignupGoogle(authGoogleRequest, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> loginGoogle([FromBody] AuthGoogleRequest authGoogleRequest, CancellationToken cancellationToken)
    {
        var result = await _auth.LoginGoogle(authGoogleRequest, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("facebook-signin")]
    public async Task<IActionResult> SigninFacebook([FromBody] AuthFacebookRequest authFacebookRequest , CancellationToken cancellationToken)
    {
        var result = await _auth.AuthenticationWithFacebook(authFacebookRequest, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

}