using Microsoft.AspNetCore.Http;

namespace FileManager.Core.DTOs;

public record UploadFileRequest(IFormFile File);
