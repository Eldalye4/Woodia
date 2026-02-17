using Mapster;
using Microsoft.EntityFrameworkCore;
using Woodia.Contract.Product;
using Woodia.Entity;
using Woodia.Enums;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class ProductService:IProductService
{
    private readonly ApplicationDbContext _context;
    public ProductService(ApplicationDbContext context)
    {
        _context = context;   
    }

    public async Task<Result<ProductResponse>> AddAsync(ProductRequest productRequest,CancellationToken cancellationToken)
    {
        
        
        var product = productRequest.Adapt<Product>();
        await _context.Products.AddAsync(product,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        var Response=await _context.Products
            .Include(x=>x.Category)
            .Include(x=>x.ProductImages)
            .FirstOrDefaultAsync(x=>x.Id==product.Id,cancellationToken);
        return Result.Success(Response.Adapt<ProductResponse>());
        
    }

    public async Task<Result<ProductResponse>> GetAsync(int Id, CancellationToken cancellationToken)
    {
        
        var product=await _context.Products
            .Include(x=>x.Category)
            .Include(x=>x.ProductImages)
            .FirstOrDefaultAsync(x=>x.Id==Id,cancellationToken);
        if (product==null)
        {
            return Result.failure<ProductResponse>(ProductError.ProductNotFound);
        }
        return Result.Success(product.Adapt<ProductResponse>());
    }

    public async Task<Result<PaginatedList<ProductListingResponse>>> GetAllAsync(RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var products = _context.Products
            .AsNoTracking()
            .Where(x=>x.ProductStatus==ProductStatus.Published)
            .Include(x=>x.ProductImages)
            .ProjectToType<ProductListingResponse>();
        var response =
            await PaginatedList<ProductListingResponse>
                .CreateAsync(products, requestFilters.PageNumber, requestFilters.PageSize,cancellationToken);


        return Result.Success(response);
    }

    public async Task<Result<PaginatedList<ProductListingResponse>>> GetByCategory(RequestFilters requestFilters,string slug, CancellationToken cancellationToken)
    {
        var categorys = new List<int>();
        
        // parent category
        if (await _context.categories.AsNoTracking().Include(x=>x.SubCategories).FirstOrDefaultAsync(x => x.Slug == slug) is not { } category)
        {
            return Result.failure<PaginatedList<ProductListingResponse>>(CategoryError.CategoryNotFound);

        }

        if (category.SubCategories != null && category.SubCategories.Any())
        {
            categorys.AddRange(category.SubCategories.Select(x=>x.Id));   
        }
        else
        {
            categorys.Add(category.Id);
        }

        var products =  _context.Products.AsNoTracking()
                                                            .Where(x => categorys.Contains(x.CategoryId) && x.ProductStatus==ProductStatus.Published)
                                                            
                                                            .Include(x => x.ProductImages)
                                                            .ProjectToType<ProductListingResponse>();
        var response =
            await PaginatedList<ProductListingResponse>
                .CreateAsync(products, requestFilters.PageNumber, requestFilters.PageSize,cancellationToken);
        
        return Result.Success(response);

    }

    public async Task<Result<ProductDetailsResponse>> GetPublicGyId(int id,CancellationToken cancellationToken)
    {
        

        var product =await _context.Products.Where(x=>x.ProductStatus==ProductStatus.Published)
            .Include(x=>x.Category)
            .Include(x=>x.ProductImages)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            return  Result.failure<ProductDetailsResponse>(ProductError.ProductNotFound);
        }
        return  Result.Success(product.Adapt<ProductDetailsResponse>());

    }

    public async Task<Result<PaginatedList<ProductGridItemResponse>>> GetAllForAdminAsync(RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var products =  _context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductImages)
            .ProjectToType<ProductGridItemResponse>();
        var response =
            await PaginatedList<ProductGridItemResponse>
                .CreateAsync(products, requestFilters.PageNumber, requestFilters.PageSize,cancellationToken);
        return Result.Success(response);
    }

    public async Task<Result> EditStatusForAdminAsync(int id,ProductStatusRequest productStatusRequest,CancellationToken cancellationToken)
    {
        var product =await _context.Products.FirstOrDefaultAsync(x => x.Id==id);
        if (product == null)
        {
            return Result.failure(ProductError.ProductNotFound);
        }

        productStatusRequest.Adapt(product);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteForAdminAsync(int id, CancellationToken cancellationToken)
    {
        if (await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) is not { } product)
        {
            return Result.failure(ProductError.ProductNotFound);
        }
        product.IsDeleted=true;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}