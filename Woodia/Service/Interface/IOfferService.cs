using Woodia.Contract.Offer;

namespace Woodia.Service.Interface;

public interface IOfferService
{
    Task<Result<OfferResponse>> AddAsync(string CarpenterId, int jobId, OfferRequest offerRequest,
        CancellationToken cancellationToken = default);
}