using FileManager.Api.Services;
using FileManager.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync([FromForm] UploadFileRequest request, CancellationToken cancellation)
    {
       var id = await _fileService.UploadFileAsync(request.File, cancellation);
        return CreatedAtAction(nameof(Download),new {id}, null);

    }
    [HttpPost("upload-many")]
    public async Task<IActionResult> UploadFilesAsync([FromForm] UploadFilesRequest request, CancellationToken cancellation)
    {
        await _fileService.UploadManyFilesAsync(request.Files, cancellation);
        return Created();

    }
    [HttpPost("upload-Image")]
    public async Task<IActionResult> UploadFilesAsync([FromForm] UploadImagesRequest request, CancellationToken cancellation)
    {
        await _fileService.UploadImageAsync(request.Image, cancellation);
        return Created();

    }
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(Guid id, CancellationToken cancellation)
    {
        var (bytes, filename, contentType) = await _fileService.DownloadAsync(id, cancellation);
        if (bytes == null)
        {
            return NotFound();
        }
        return File(bytes, contentType, filename);
    }
    [HttpGet("stream/{id}")]
    public async Task<IActionResult> Stream(Guid id, CancellationToken cancellation)
    {
        var (stream, filename, contentType) = await _fileService.StreamAsync(id, cancellation);
        if (stream == null)
        {
            return NotFound();
        }
        return File(stream, contentType,filename,true);
    }
}
