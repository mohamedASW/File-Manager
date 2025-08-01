namespace FileManager.Api.Services;

public interface IFileService
{
    Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellation = default);
    Task UploadManyFilesAsync(IFormFileCollection files, CancellationToken cancellation = default);
    Task<(byte[]? bytes, string filename, string contenType)> DownloadAsync(Guid id, CancellationToken cancellationToken = default);
    Task UploadImageAsync(IFormFile file, CancellationToken cancellation = default);
    Task<(Stream? stream, string filename, string contentType)> StreamAsync(Guid id, CancellationToken cancellationToken = default);
  
}
