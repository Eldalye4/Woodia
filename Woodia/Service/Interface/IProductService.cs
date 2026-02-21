using Woodia.Contract.Product;

namespace Woodia.Service.Interface;

public interface IProductService
{
    Task<Result<ProductResponse>> AddAsync(ProductRequest productRequest, CancellationToken cancellationToken);
    Task<Result<ProductResponse>> GetAsync(int Id, CancellationToken cancellationToken);

    Task<Result<PaginatedList<ProductListingResponse>>> GetByCategory(RequestFilters requestFilters, string slug,
        CancellationToken cancellationToken);

    Task<Result<PaginatedList<ProductListingResponse>>> GetAllAsync(RequestFilters requestFilters,CancellationToken cancellationToken);
    Task<Result<ProductDetailsResponse>> GetPublicGyId(int id, CancellationToken cancellationToken);

    Task<Result<PaginatedList<ProductGridItemResponse>>> GetAllForAdminAsync(RequestFilters requestFilters,
        CancellationToken cancellationToken);

    Task<Result> EditStatusForAdminAsync(int id, ProductStatusRequest productStatusRequest,
        CancellationToken cancellationToken);
    Task<Result> DeleteForAdminAsync(int id, CancellationToken cancellationToken);

    Task<Result> UpdateForAdminAsync(int id, ProductUpdateRequest productUpdateRequest,
        CancellationToken cancellationToken);
}