using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Woodia.Factory;

public interface IUploadParamsFactory
{
    Task<string> UploadAsync(IFormFile file, Cloudinary cloudinary, CancellationToken cancellationToken);
}