using Microsoft.AspNetCore.Http;

namespace FileManager.Core.DTOs;

public record UploadFilesRequest(IFormFileCollection Files);
