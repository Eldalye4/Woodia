using System.Globalization;
using System.Text.Json.Nodes;
using CloudinaryDotNet;
using Mapster;
using Woodia.Contract.Carpenter;
using Woodia.Contract.Product;
using Woodia.Entity;
using Woodia.Enums;

public class ProductMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<JsonNode, JsonNode>.NewConfig().MapWith(src => src);
        config.NewConfig<Category, CategoryProductResponse>()
            .Map(dest => dest.NameAr, src => src.NameAr)
            .Map(dest => dest.NameEn, src => src.NameEn);
        config.NewConfig<ProductImage, ProductImageResponse>();


        config.NewConfig<Product, ProductResponse>()
            .Map(dest => dest.ProductLineAr, src => src.ProductLine.ToLocalizedString(true))
            .Map(dest => dest.ProductLineEn, src => src.ProductLine.ToLocalizedString(false))
            .Map(dest => dest.ProductImageResponses, src => src.ProductImages.OrderBy(x => x.DisplayOrder))
            .Map(dest => dest.CategoryProductResponses, src => src.Category)
            .Map(dest => dest.ModelConfig, src => ValidateJson(src.ModelConfig));
        

        config.NewConfig<ProductRequest, Product>()
            .Map(dest => dest.ProductLine, src => src.ProductLine)
            .Map(dest => dest.CategoryId, src => src.CategoryId )
            .Map(dest=>dest.DescriptionAr, src => src.DescriptionAr)
            .Map(dest=>dest.DescriptionEn, src => src.DescriptionEn)
            .Map(dest => dest.ProductImages, src =>
                src.ProductImage.Select((img, index) =>
                    new ProductImage
                    {
                        Url = img.Url,
                        ImagePlace = Enum.Parse<ImagePlace>(img.imagePlace, true),
                        DisplayOrder = index + 1
                    }))
            .Map(dest => dest.ModelConfig, src => src.ModelConfig.ToString());
        
        

        config.NewConfig<Product, ProductListingResponse>()
            .Map(dest => dest.HoverImage, src => src.ProductImages
                .Where(x => x.ImagePlace == ImagePlace.Hover)
                .Select(x => x.Url)
                .FirstOrDefault())
            .Map(dest => dest.ThumbnailImage, src => src.ProductImages
                .Where(x => x.ImagePlace == ImagePlace.Thumbnail)
                .Select(x => x.Url)
                .FirstOrDefault())
            .Map(dest=>dest.ProductLine,src=>
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")?
                    src.ProductLine.ToLocalizedString(true):
                    src.ProductLine.ToLocalizedString(false))
            .Map(dest=>dest.Description,src=>
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")?
                    src.DescriptionAr:
                    src.DescriptionEn);

        config.NewConfig<Product, ProductDetailsResponse>()
            .Map(dest => dest.ModelConfig, src => ValidateJson(src.ModelConfig))
            .Map(dest=>dest.Images,src=>src.ProductImages
                .Where(x=>x.ImagePlace==ImagePlace.Gallery)
                .OrderBy(x=>x.DisplayOrder)
                .Select(x=>x.Url)
                .ToList());
        config.NewConfig<Category, CategorySummaryResponse>()
            .Map(dest => dest.Name, src => src.NameEn);
        
       config.NewConfig<Product,ProductGridItemResponse>()
           .Map(dest=>dest.ProductStatus,src=>src.ProductStatus)
           .Map(dest=>dest.Category,src=>CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")?
               src.Category.NameAr:
               src.Category.NameEn)
           .Map(dest=>dest.ImageThumbnail,src=>src.ProductImages
                                               .Where(x=>x.ImagePlace==ImagePlace.Thumbnail)
                                               .Select(x=>x.Url)
                                               .FirstOrDefault())
           .Map(dest=>dest.ProductLine,src=>
               CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("ar")?
                   src.ProductLine.ToLocalizedString(true):
                   src.ProductLine.ToLocalizedString(false));

       config.NewConfig<ProductStatusRequest, Product>()
           .Map(dest => dest.ProductStatus, src => src.Status);
    }

    private static JsonNode ValidateJson(string jsonٍstirng)
    {
        if (string.IsNullOrWhiteSpace(jsonٍstirng))
        {
            return null;
        }

        try
        {
            return JsonNode.Parse(jsonٍstirng);
        }
        catch 
        {
            return null;
        }
    }
}