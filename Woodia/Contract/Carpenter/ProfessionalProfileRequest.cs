namespace Woodia.Contract.Carpenter;

public record ProfessionalProfileRequest(
    decimal HourlyRate,
    string ProfileOverview,
    string ProfileImageUrl);