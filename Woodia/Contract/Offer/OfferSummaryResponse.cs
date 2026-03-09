namespace Woodia.Contract.Offer;

public record OfferSummaryResponse(
    int Id,
    decimal Price,
    string Description,
    int DeliveryDay,
    string Status,
    string JobTitle,
    string CreatedAt
    );

