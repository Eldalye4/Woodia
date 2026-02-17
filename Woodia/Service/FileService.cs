using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Woodia.Abstractions.FileConfiguration;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class FileService:Ifile
{
    
    private readonly Cloudinary _cloudinary;
    private readonly CloundarySettings _cloundarySettings ;

    public FileService(IOptions<CloundarySettings> cloundarySettings)
    {
        _cloundarySettings = cloundarySettings.Value;
        var account = new Account(_cloundarySettings.Cloud, _cloundarySettings.ApiKey, _cloundarySettings.ApiSecret);
        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }
    
    
    
    public async Task<List<string>> UploadManyFile(IFormFileCollection files, CancellationToken cancellationToken)
    {
        List<string> url = [];
        foreach (var file in files)
        {


            var result = await UploadFile(file, cancellationToken);
            url.Add(result);
        }
        return url;
    }
    private async Task<string> UploadFile(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;
        var uploadparams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, memoryStream),
        };


        var result = _cloudinary.Upload(uploadparams);

        if (result.Error != null)
        {
            throw new Exception($"Cloudinary error occured: {result.Error.Message}");
        }
        return result.SecureUrl.ToString();
    }
}