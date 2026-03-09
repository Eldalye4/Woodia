namespace Woodia.Contract.Offer;

public record JobOfferDetailsResponse( 
    int Id,
    string CarpenterId,
    string CarpenterName,
    decimal Price,
    string Description,
    int DeliveryDay,
    string Status
    ,string CreatedAt);