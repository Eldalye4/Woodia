public interface ICategory
{
    Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken);
    Task<Result<CategoryNavigationResponse>> GetNavigation(string slug, CancellationToken cancellationToken);
    Task<List<CategoryParentResponse>> GetParent(CancellationToken cancellationToken);
}