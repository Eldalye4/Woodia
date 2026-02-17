
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
        config.NewConfig<Category, CategoryProductResponse>();
        config.NewConfig<ProductImage, ProductImageResponse>();


        config.NewConfig<Product, ProductResponse>()
            .Map(dest => dest.ProductLine, src => src.ProductLine)
            .Map(dest => dest.ProductImageResponses, src => src.ProductImages)
            .Map(dest => dest.CategoryProductResponses, src => src.Category)
            .Map(dest => dest.ModelConfig, src => ValidateJson(src.ModelConfig));


        config.NewConfig<ProductRequest, Product>()
            .Map(dest => dest.ProductLine, src => src.ProductLine)
            .Map(dest => dest.CategoryId, src => src.CategoryId
                
            )
    

   
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
                .FirstOrDefault());
        
        config.NewConfig<Product,ProductDetailsResponse>()
            .Map(dest => dest.ModelConfig, src => ValidateJson(src.ModelConfig))
            .Map(dest=>dest.Category,src=>src.Category.Name)
            .Map(dest=>dest.Images,src=>src.ProductImages.Where(x=>x.ImagePlace==ImagePlace.Gallery).Select(x=>x.Url).ToList());
        
       config.NewConfig<Product,ProductGridItemResponse>()
           .Map(dest=>dest.ProductStatus,src=>src.ProductStatus)
           .Map(dest=>dest.Category,src=>src.Category.Name)
           .Map(dest=>dest.ImageThumbnail,src=>src.ProductImages
                                               .Where(x=>x.ImagePlace==ImagePlace.Thumbnail)
                                               .Select(x=>x.Url)
                                               .FirstOrDefault());

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