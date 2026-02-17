using Woodia.Contract.Category;
using Woodia.Contract.Product;

public interface ICategory
{
    Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken);
    Task<Result<CategoryNavigationResponse>> GetNavigation(string slug, CancellationToken cancellationToken);
    Task<List<CategoryParentResponse>> GetParent(CancellationToken cancellationToken);
    Task<Result<PaginatedList<AdminCategoryListingResponse>>> GetAllForAdminAsync(RequestFilters requestFilters,
        CancellationToken cancellationToken);

    Task<Result<AdminCategoryResponse>> GetForAdminAsync(int Id, CancellationToken cancellationToken);
    Task<Result<List<ParentCategoryResponse>>> GetParentsForAdminAsync(CancellationToken cancellationToken);

    Task<Result<AdminCategoryResponse>> AddForAdminAsync(CategoryRequest categoryRequest,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int id, CategoryRequest categoryRequest, CancellationToken cancellationToken);
}

