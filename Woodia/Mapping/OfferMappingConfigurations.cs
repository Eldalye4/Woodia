
using System.Text.Json.Nodes;
using CloudinaryDotNet;
using Mapster;
using Woodia.Contract.Carpenter;
using Woodia.Contract.Product;
using Woodia.Entity;
using Woodia.Enums;

public class CategoryMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, AdminCategoryListingResponse>()
            .Map(dest => dest.ImageThumbnail, src => src.ImageUrl)
            .Map(dest => dest.CategoryName, src => src.ParentId != null
                ? $"{src.Parent.Name} >> {src.Name}"
                : src.Name)
            .Map(x=>x.CategoryId, src => src.Id);
    }
}