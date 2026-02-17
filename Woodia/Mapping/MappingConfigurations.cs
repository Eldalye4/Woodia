
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
        .Map(dest => dest.ChildCategory, src => src.SubCategories);

        config.NewConfig<Category, CategoryNavigationResponse>()
        .Map(dest => dest.CategoryChildNavigationResponses,
                 src => src.ParentId == null ?
                src.SubCategories : src.Parent!.SubCategories);

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
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<CategoryRequest, Category>()
            .Map(src => src.ImageUrl, dest => dest.ImageThumbnail);
        config.NewConfig<ProfessionalProfileRequest, CarpenterProfile>()
            .Map(dest => dest.ProfileImageUrl, src => src.ProfileImageUrl);
        
        config.NewConfig<CarpenterProfile,ProfessionalProfileResponse>()
            .Map(dest => dest.ProfileImageUrl, src => src.ProfileImageUrl);
    }
}