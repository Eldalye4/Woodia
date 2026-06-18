using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Woodia.Abstractions.FileConfiguration;
using Woodia.Factory;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class FileService : Ifile
{

    private readonly Cloudinary _cloudinary;
    private readonly CloundarySettings _cloundarySettings;
    private readonly IUploadParamsFactory _uploadParamsFactory;
    public FileService(IOptions<CloundarySettings> cloundarySettings, IUploadParamsFactory iUploadParamsFactory)
    {
        _uploadParamsFactory = iUploadParamsFactory;
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
        return await _uploadParamsFactory.UploadAsync(file, _cloudinary, cancellationToken);
    }
}