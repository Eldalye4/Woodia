
namespace Woodia.Contract.Offer;

public record OfferResponse(
    int Id,
    string CarpenterId,
    decimal Price,
    string Description,
    int DeliveryDay,
    string Status
    ,DateTime CreatedAt);