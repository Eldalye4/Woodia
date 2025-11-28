public interface IClientService
{
    Task<Result<ClientProfileResponse>> GetProfile(String UserId);
    Task<Result> UpdateProfile(string userId, ProfileRequest request);
    Task<Result> UpadateEmail(string userId, EmailUpdateRequest emailUpdate);
    Task<Result> ResendEmail(string userId, string email);
    Task<Result> VerifyEmail(string userId, ConfirmEmailRequest confirmEmailRequest);
    Task<Result<ContactInformationResponse>> AddContactInformation(string userId, ContactInformationRequest request, CancellationToken cancellationToken);
    Task<Result<ContactInformationResponse>> GetContactInformation(int id, CancellationToken cancellationToken);
    Task<Result> UpdateContactInformation(int id, ContactInformationRequest request, CancellationToken cancellationToken);
}