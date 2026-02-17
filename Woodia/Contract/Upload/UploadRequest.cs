namespace Woodia.Contract.Upload;

public record UploadRequest(
    IFormFileCollection Files
);