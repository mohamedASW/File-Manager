using Microsoft.AspNetCore.Http;

namespace FileManager.Core.DTOs;

public record UploadImagesRequest(IFormFile Image);
