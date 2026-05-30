namespace Woodia.Contract.Carpenter;

public record CompleteProfileResponse(
    string FirstName,
    string LastName ,
    string? PhotoUrl,
    ProfessionalProfileResponse ProfessionalProfile,
    List<PortfolioItemResponse> PortfolioItem
    );