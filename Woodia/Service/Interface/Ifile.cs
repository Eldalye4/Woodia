namespace Woodia.Service.Interface;

public interface Ifile
{
    Task<List<string>> UploadManyFile(IFormFileCollection files, CancellationToken cancellationToken);
}