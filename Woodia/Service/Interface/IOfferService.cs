using Woodia.Contract.Offer;

namespace Woodia.Service.Interface;

public interface IOfferService
{
    Task<Result<OfferResponse>> AddAsync(string CarpenterId, int jobId, OfferRequest offerRequest,
        CancellationToken cancellationToken = default);

   Task<Result<OfferDetailsResponse>> GetAsync(string carpenterId, int id,
        CancellationToken cancellationToken = default);

    Task<Result<PaginatedList<OfferSummaryResponse>>> GetAllAsync(RequestFilters requestFilters,
        string carpenterId, CancellationToken cancellationToken = default);

    Task<Result> withdrawStatusAsync(string carpenterId, int id,
        CancellationToken cancellationToken = default);

    Task<Result<PaginatedList<JobOfferSummaryResponse>>> GetAllOfferByJobAsync(RequestFilters requestFilters,
        int jobId, string ClientId, CancellationToken cancellationToken = default);

    Task<Result<JobOfferDetailsResponse>> GetOfferByJobAsync(
        int jobId, string ClientId, int offerId, CancellationToken cancellationToken = default);

}