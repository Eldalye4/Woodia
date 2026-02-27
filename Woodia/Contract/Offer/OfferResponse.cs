
namespace Woodia.Contract.Offer;

public record JobResponse(
    int Id,
    string CarpenterId,
    decimal Price,
    string Description,
    int DeliveryDay,
    string Status);