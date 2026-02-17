namespace Woodia.Contract.Carpenter;

public record ProfessionalProfileResponse(
    int Id,
    decimal HourlyRate,
    string ProfileOverview,
    string ProfileImageUrl);