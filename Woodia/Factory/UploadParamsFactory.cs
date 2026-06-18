using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Woodia.Factory;

public class UploadParamsFactory: IUploadParamsFactory
{
    public async Task<string> UploadAsync(IFormFile file, Cloudinary cloudinary, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;
        var fileDescription = new FileDescription(file.FileName, memoryStream);

        if (file.ContentType.StartsWith("video/") || file.ContentType.StartsWith("audio/"))
        {
            var result = await cloudinary.UploadLargeAsync(
                new VideoUploadParams { File = fileDescription },
                6000000,
                cancellationToken: cancellationToken);

            if (result.Error != null) throw new Exception(result.Error.Message);
            return result.SecureUrl.ToString();
        }
        else
        {
            var result = await cloudinary.UploadAsync(
                new ImageUploadParams { File = fileDescription },
                cancellationToken);

            if (result.Error != null) throw new Exception(result.Error.Message);
            return result.SecureUrl.ToString();
        }
    }
}