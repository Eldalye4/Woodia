public interface IAuth
{
    Task<Result> Register(RegisterRequest request, CancellationToken cancellationToken);
    Task<Result> ConfirmEmail(ConfirmEmailRequest request, CancellationToken cancellationToken);
    Task<Result<AuthRespond>> GetToken(string Email, string Password, CancellationToken cancellationToken);
    Task<Result<AuthRespond>> GetRefreshToken(string token, string refreshToken, CancellationToken cancellationToken);
    Task<Result> RensendEmail(ResendRequest request);
    Task<Result<AuthRespond>> AuthenticationWithGoogle(AuthGoogleRequest authGoogleRequest, CancellationToken cancellationToken);
    Task<Result<AuthRespond>> AuthenticationWithFacebook(AuthFacebookRequest authFacebookRequest, CancellationToken cancellationToken);
}   