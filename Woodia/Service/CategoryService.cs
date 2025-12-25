
using Mapster;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategory
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ApplicationDbContext context, ILogger<CategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _context.categories
            .Where(x => x.ParentId == null)
            .AsNoTracking()
            .ProjectToType<CategoryResponse>()
            .ToListAsync(cancellationToken);
        return result;
    }
    public async Task<Result<CategoryNavigationResponse>> GetNavigation(string slug, CancellationToken cancellationToken)
    {
        var category = await _context.categories
                    .Include(x => x.SubCategories)

                    .Include(x => x.Parent)
                        .ThenInclude(x => x.SubCategories)
                    .Where(x => x.Slug == slug)
                    .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            return Result.failure<CategoryNavigationResponse>(CategoryError.CategoryNotFound);
        }
        var response = category.Adapt<CategoryNavigationResponse>();
        return Result.Success(response);
    }

    public async Task<List<CategoryParentResponse>> GetParent(CancellationToken cancellationToken)
    => await _context.categories
        .AsNoTracking()
        .Where(x => x.ParentId == null)
        .ProjectToType<CategoryParentResponse>()
        .ToListAsync(cancellationToken);

}