
using FileManager.Core;
using FileManager.Core.Entites;
using System.Threading;

namespace FileManager.Api.Services;

public class FileService(IWebHostEnvironment webHost, IUnitOfWork unitOfWork) : IFileService
{
    private readonly string _filesPath = $"{webHost.WebRootPath}/uploads";
    private readonly string _imagesPath = $"{webHost.WebRootPath}/images";
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<(byte[]? bytes, string filename, string contenType)> DownloadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _unitOfWork.FileRepository.GetByIdAsync(id, cancellationToken);
        if (file == null)
            return (null, string.Empty, string.Empty);
        using var memoryStream = new MemoryStream();
        var path = Path.Combine(_filesPath, file.StoredFileName);
        await using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            await stream.CopyToAsync(memoryStream, cancellationToken);
        }
        return (memoryStream.ToArray(), file.FileName, file.ContentType);

    }

    public async Task<(Stream? stream, string filename, string contentType)> StreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _unitOfWork.FileRepository.GetByIdAsync(id, cancellationToken);
        if (file == null)
            return (null, string.Empty, string.Empty);
        var path = Path.Combine(_filesPath, file.StoredFileName);
        var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return (stream, file.FileName, file.ContentType);
    }

    public async Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellation = default)
    {
        var uploadedFile = await saveFile(file, cancellation);
        await _unitOfWork.FileRepository.AddAsync(uploadedFile, cancellation);
        await _unitOfWork.CompleteAsync(cancellation);
        return uploadedFile.Id;
    }

    public async Task UploadImageAsync(IFormFile file, CancellationToken cancellation = default)
    {
        var imagepath = Path.Combine(_imagesPath, file.FileName);

        using var filestream = new FileStream(imagepath, FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(filestream, cancellation);
    }

    public async Task UploadManyFilesAsync(IFormFileCollection files, CancellationToken cancellation = default)
    {
        List<UploadedFile> uploadedFiles = [];
        foreach (var file in files)
            uploadedFiles.Add(saveFile(file, cancellation).Result);
        await _unitOfWork.FileRepository.AddRangeAsync(uploadedFiles, cancellation);
        await _unitOfWork.CompleteAsync(cancellation);
    }


    private async Task<UploadedFile> saveFile(IFormFile file, CancellationToken cancellation)
    {
        var randomFileName = Path.GetRandomFileName();

        var uploadedFile = new UploadedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            StoredFileName = randomFileName,
            FileExtension = Path.GetExtension(file.FileName)
        };

        var path = Path.Combine(_filesPath, randomFileName);

        using var stream = File.Create(path);
        await file.CopyToAsync(stream, cancellation);
        return uploadedFile;

    }
}
