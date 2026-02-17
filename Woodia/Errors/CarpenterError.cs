namespace Woodia.Errors;

public static class CarpenterError
{
    public static readonly Error ConflictProfile =
        new("Profile.Conflict", "User Already Has A Professional Profile.", StatusCodes.Status409Conflict);

    public static readonly Error ProfileNotComplete =
        new("Profile.Incomplete", "Carpenter profile setup required. Please complete your hourly rate and overview.",
            StatusCodes.Status403Forbidden);
    
    public static readonly Error PortfolioItemNotFound =
        new("PortfolioItem.NotFound", "No PortfolioItem was found with the given ID", StatusCodes.Status404NotFound);


}