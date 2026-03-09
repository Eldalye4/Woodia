namespace Woodia.Contract.Offer;

public record JobOfferSummaryResponse(
    int Id,
    string CarpenterId,
    string CarpenterName,
    decimal Price,
    string Description,
    int DeliveryDay,
    string Status
    ,string CreatedAt);