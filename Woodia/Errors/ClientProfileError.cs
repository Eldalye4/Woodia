
public static class ClientProfileError
{
    public static readonly Error ContactNotFound =
    new Error("ContactInformation.NotFound", "No contactinformation was found with the given ID", StatusCodes.Status404NotFound);
    public static readonly Error PhotoAlreadyExists =
        new("PhotoProfile.PhotoAlreadyExists","User already has a profile picture",StatusCodes.Status409Conflict);

}