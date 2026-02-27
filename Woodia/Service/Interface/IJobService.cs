using Woodia.Contract.Job;

namespace Woodia.Service.Interface;

public interface IJobService
{
    Task<Result<JobResponse>> AddAsync(string UserId, JobRequest jobRequest, CancellationToken cancellationToken);

    Task<Result<PaginatedList<JoblistingResponse>>> GetAllForClientAsync(RequestFilters requestFilters,
        string userId, CancellationToken cancellationToken);

    Task<Result<JobResponse>> GetForClientAsync(string clientID,int Id, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(string clientID,int Id, JobRequest jobRequest,
        CancellationToken cancellationToken);

    Task<Result> InProgressStateAsync(string clientID, int Id, CancellationToken cancellationToken);
    Task<Result> CompleteStateAsync(string clientID, int Id, CancellationToken cancellationToken);
    Task<Result> CanceleStateAsync(string clientID, int Id, CancellationToken cancellationToken);

    Task<Result<PaginatedList<CarpenterJobListingResponse>>> GetAllForCarpenterAsync(RequestFilters requestFilters,
        CancellationToken cancellationToken);

    Task<Result<CarpenterJobResponse>> GetForCarpenterAsync(int Id, CancellationToken cancellationToken);
}