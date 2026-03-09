namespace Woodia.Errors;

public static class OfferError
{
    public static readonly Error DuplicateOffer =
        new Error("Offer.Duplicate", 
            "The carpenter has already submitted an offer for this job", StatusCodes.Status409Conflict);
    public static readonly Error OfferNotFound = new
        ("Offer.OfferNotFound", "The requested Offer was not found.", StatusCodes.Status404NotFound);
    public static readonly Error AlreadyWithdrawn = new(
        "Offer.AlreadyWithdrawn", 
        "This offer has already been withdrawn.",
        StatusCodes.Status409Conflict
    );
    public static readonly Error CannotWithdraw = new(
        "Offer.CannotWithdraw", 
        "Cannot withdraw the offer because a decision has already been made (accepted or rejected).",
            StatusCodes.Status400BadRequest
    );
    public static readonly Error InvalidOfferStatus = 
        new("Offer.InvalidStatus", 
            "Cannot modify the offer status because it is not in the Pending or Interviewing state.",
            StatusCodes.Status400BadRequest);
}
