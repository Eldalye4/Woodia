
using Mapster;
using Microsoft.EntityFrameworkCore;
using Woodia.Contract.Category;
using Woodia.Contract.Product;

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
            .Where(x => x.ParentId == null  )
            .AsNoTracking()
            .ProjectToType<CategoryResponse>()
            .ToListAsync(cancellationToken);
        return result;
    }
    public async Task<Result<CategoryNavigationResponse>> GetNavigation(string slug, CancellationToken cancellationToken)
    {
        var category = await _context.categories
            .Where(x => x.Slug == slug  )
                    .Include(x => x.SubCategories)

                    .Include(x => x.Parent)
                        .ThenInclude(x => x.SubCategories)
                  
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
        .Where(x => x.ParentId == null )
        .ProjectToType<CategoryParentResponse>()
        .ToListAsync(cancellationToken);

    public async Task<Result<PaginatedList<AdminCategoryListingResponse>>> GetAllForAdminAsync(RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var category =  _context.categories
            .Include(x=>x.SubCategories)
            .ProjectToType<AdminCategoryListingResponse>();
        var response = await PaginatedList<AdminCategoryListingResponse>
            .CreateAsync(category, requestFilters.PageNumber, requestFilters.PageSize, cancellationToken);
        return Result.Success(response);
    }

    public async Task<Result<AdminCategoryResponse>> GetForAdminAsync(int Id, CancellationToken cancellationToken)
    {
        var category = await _context.categories
                .Where(x => x.Id == Id )
                                                .ProjectToType<AdminCategoryResponse>()
                                                .FirstOrDefaultAsync(cancellationToken);
        if (category == null)
        {
            return Result.failure<AdminCategoryResponse>(CategoryError.CategoryNotFound);
        }
       
        return Result.Success(category);
    }

    public async Task<Result<List<ParentCategoryResponse>>> GetParentsForAdminAsync(CancellationToken cancellationToken)
    {
        var category = await _context.categories.AsNoTracking()
            .Where(x => x.ParentId == null )
            
            .ProjectToType<ParentCategoryResponse>()
            .ToListAsync(cancellationToken);
        return Result.Success(category);
    }

    public async Task<Result<AdminCategoryResponse>> AddForAdminAsync(CategoryRequest categoryRequest,
        CancellationToken cancellationToken)
    {
        var category =  categoryRequest.Adapt<Category>();
        await _context.categories.AddAsync(category,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        var categoryResponse = await _context.categories
            .Where(x => x.Id == category.Id)
            .ProjectToType<AdminCategoryResponse>()
            .FirstOrDefaultAsync(cancellationToken);
        return Result.Success(categoryResponse!);
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (await _context.categories.FirstOrDefaultAsync(x => x.Id == id,cancellationToken) is not { } category)
        {
            return Result.failure(CategoryError.CategoryNotFound);
        }
        category.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }

    public async Task<Result> UpdateAsync(int id,CategoryRequest categoryRequest, CancellationToken cancellationToken)
    {
        
        if (await _context.categories.FirstOrDefaultAsync(x => x.Id == id,cancellationToken) is not { } category)
        {
            return Result.failure(CategoryError.CategoryNotFound);
        }

        if (categoryRequest.ParentId == id)
        {
            return Result.failure(CategoryError.InvalidParentCategory);
        }
        if (categoryRequest.ParentId is not null && !await _context.categories.AnyAsync(x=>x.Id==categoryRequest.ParentId,cancellationToken)  )
        {
            return Result.failure(CategoryError.ParentCategoryNotFound);
        }

        categoryRequest.Adapt(category);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    
    
}