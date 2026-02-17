using Woodia.Contract.Carpenter;

namespace Woodia.Service.Interface;

public interface ICarpenterService
{
    Task<Result<ProfessionalProfileResponse>> AddProfessionalProfile(string userId,
        ProfessionalProfileRequest professionalProfile);

    Task<Result> UpdateProfessionalProfile(string userId, ProfessionalProfileRequest professionalProfile);
    Task<Result<ProfessionalProfileResponse>> GetProfessionalProfile(string userId);

    Task<Result<PortfolioItemResponse>> AddPortfolioItem(string userId, PortfolioItemRequest portfolioItem,
        CancellationToken cancellationToken);

    Task<Result<PortfolioItemResponse>> GetPortfolioItem(int Id,CancellationToken cancellationToken);

    Task<Result> UpdatePortfolioItem(int Id, PortfolioItemRequest portfolioItemRequest,
        CancellationToken cancellationToken);

    Task<Result> DeletePortfolioItem(int Id, CancellationToken cancellationToken);

    Task<Result<PaginatedList<PortfolioItemResponse>>> GetAllAsync(string userId, RequestFilters requestFilters,
        CancellationToken cancellationToken);

    

}