public interface IAuth
{
    Task<Result> Register(RegisterRequest request, CancellationToken cancellationToken);
    Task<Result> ConfirmEmail(ConfirmEmailRequest request, CancellationToken cancellationToken);
    Task<Result<AuthRespond>> GetToken(string Email, string Password, CancellationToken cancellationToken);
    Task<Result<AuthRespond>> GetRefreshToken(string token, string refreshToken, CancellationToken cancellationToken);
}   