namespace Woodia.Contract.Offer;

public record OfferDetailsResponse(
    int Id,
    decimal Price,
    string Description,
    int DeliveryDay,
    string Status,
    JobSummaryDto JobSummary,
    string CreatedAt
);