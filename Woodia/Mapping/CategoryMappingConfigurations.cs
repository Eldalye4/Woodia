
using System.Globalization;
using System.Text.Json.Nodes;
using CloudinaryDotNet;
using Mapster;
using Woodia.Contract.Carpenter;
using Woodia.Contract.Category;
using Woodia.Contract.Product;
using Woodia.Entity;
using Woodia.Enums;

public class CategoryMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, AdminCategoryListingResponse>()
            .Map(dest => dest.ImageThumbnail, src => src.ImageUrl)
            .Map(dest => dest.NameAr, src => src.ParentId != null
                ? $"{src.Parent.NameAr} >> {src.NameAr}"
                : src.NameAr)
            .Map(dest => dest.NameEn, src => src.ParentId != null
                ? $"{src.Parent.NameEn} >> {src.NameEn}"
                : src.NameEn)
            .Map(x=>x.CategoryId, src => src.Id);
        config.NewConfig<Category, CategoryChildForAdminResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src =>
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar") ? src.NameAr : src.NameEn);
    }
}