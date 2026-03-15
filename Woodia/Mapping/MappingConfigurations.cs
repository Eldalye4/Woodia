
using System.Globalization;
using CloudinaryDotNet;
using Mapster;
using Woodia.Contract.Carpenter;
using Woodia.Contract.Category;
using Woodia.Entity;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, ApplicationUser>()
        .Map(dest => dest.UserName, src => src.Email)
        .Map(dest => dest.userType, src => src.Type);

        config.NewConfig<ContactInformation, ContactInformationResponse>()
        .Map(dest => dest.Governate, src => src.governorate.NameEn)
        .Map(dest => dest.UserId, src => src.UserId);


        config.NewConfig<Category, CategoryResponse>()
        .Map(dest => dest.ChildCategory, src => src.SubCategories)
        .Map(dest=>dest.Name,src=>
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar") ?src.NameAr:src.NameEn)
        .Map(dest=>dest.Slug,src=>
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar") ?src.SlugAr:src.SlugEn);
        config.NewConfig<Category,CategoryChildResponse>()
            .Map(dest=>dest.Name,src=>
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar") ?src.NameAr:src.NameEn)
            .Map(dest=>dest.Slug,src=>
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar") ?src.SlugAr:src.SlugEn);
            
    
        config.NewConfig<Category, CategoryNavigationResponse>()
        .Map(dest => dest.CategoryChildNavigationResponses,
                 src => src.ParentId == null ?
                src.SubCategories : src.Parent!.SubCategories)
        .Map(dest=>dest.Name,src=>CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar") ?src.NameAr:src.NameEn);

        config.NewConfig<Category, CategoryChildNavigationResponse>()
            .Map(dest => dest.Name,
                src => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")
                    ? src.NameAr
                    : src.NameEn)
            .Map(dest => dest.Slug,
                src => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")
                    ? src.SlugAr
                    : src.SlugEn);
        
        config.NewConfig<Category,CategoryParentResponse>()
            .Map(dest => dest.Name,
                src => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")
                    ? src.NameAr
                    : src.NameEn)
            .Map(dest => dest.Slug,
                src => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")
                    ? src.SlugAr
                    : src.SlugEn);
        
        config.NewConfig<ApplicationUser, ContactInformationResponse>()
        .Map(dest => dest.UserId, src => src.Id)
        .Map(dest => dest.AdditionalInfo, src => src.contactInformation.AdditionalInfo)
        .Map(dest => dest.AddressLine, src => src.contactInformation.AddressLine)
        .Map(dest => dest.Governate, src => src.contactInformation.governorate.NameEn)
        .Map(dest => dest.Id, src => src.contactInformation.Id)
        .Map(dest => dest.PhoneNumber, src => src.contactInformation.PhoneNumber);

        config.NewConfig<PortfolioItemRequest, PortfolioItem>()
            .Map(dest => dest.PortfolioItemImages, src => src.PortfolioImageUrls.Select(url => new PortfolioItemImage { Url = url }));
        config.NewConfig<PortfolioItem, PortfolioItemResponse>()
            .Map(dest => dest.PortfolioImageUrls, src => src.PortfolioItemImages.Select(x => x.Url));

        config.NewConfig<Category, AdminCategoryResponse>()
            .Map(dest => dest.ImageThumbnail, src => src.ImageUrl)
            .Map(dest => dest.ParentCategory,
                src=>src.Parent);
        config.NewConfig<Category, ParentCategoryResponse>()
            .Map(src => src.Id, dest => dest.Id)
            .Map(dest => dest.NameAr, src => src.NameAr)
            .Map(dest => dest.NameEn, src => src.NameEn);

        config.NewConfig<CategoryRequest, Category>()
            .Map(src => src.ImageUrl, dest => dest.ImageThumbnail);
        
    }
}