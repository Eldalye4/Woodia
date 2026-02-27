
namespace Woodia.Contract.Offer;

public record OfferRequest(
    decimal Price,
    string Description,
    int DeliveryDay,
    string Status
    );
    
