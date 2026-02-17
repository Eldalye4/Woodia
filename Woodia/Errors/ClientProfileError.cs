
public static class ClientProfileError
{
    public static readonly Error ContactNotFound =
    new Error("ContactInformation.NotFound", "No contactinformation was found with the given ID", StatusCodes.Status404NotFound);
}