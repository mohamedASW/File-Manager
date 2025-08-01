using FileManager.Core.DTOs;
using FileManager.Core.Entites;
using FileManager.Core.Settings;
using FluentValidation;
using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core.Validations;
public class FileValidator : AbstractValidator<UploadFileRequest>
{
    public FileValidator(IOptions<FileSettings> options)
    {
        RuleFor(x => x.File)
            .SetValidator(new FileSizeValidator(options))
            .SetValidator(new BlockedFileMimeTypeValidator(options))
            .SetValidator(new BlockedFileExtensionsValidator(options));

    }
}
public class FileSizeValidator : AbstractValidator<IFormFile>
{
    public FileSizeValidator(IOptions<FileSettings> options)
    {
        RuleFor(x => x)
            .Must((file) =>
            {
                return file.Length <= options.Value.MaxFileSize;
            })
            .WithMessage($"File size must be less than or equal  {options.Value.MaxFileSize} bytes.")
            .When(x=>x is not null);
           
               
    }
}
public class BlockedFileMimeTypeValidator : AbstractValidator<IFormFile>
{
    public BlockedFileMimeTypeValidator(IOptions<FileSettings> options)
    {
        RuleFor(x => x)
            .Must((file , _ , context) =>
            {
                var stream = file.OpenReadStream();
                var detectedMime = MimeGuesser.GuessMimeType(stream);
                context.MessageFormatter.AppendArgument("mimetype", detectedMime);
                return options.Value.BlockedMimeTypes.Contains(detectedMime, StringComparer.OrdinalIgnoreCase) == false;
            })
             .WithMessage("File type {mimetype} is Blocked ")
             .When(x => x is not null); ;
    }
}
public class BlockedFileExtensionsValidator : AbstractValidator<IFormFile>
{
    public BlockedFileExtensionsValidator(IOptions<FileSettings> options)
    {
        RuleFor(x => x)
            .Must((file) =>
            {
                var type = file.ContentType;
                var extension = Path.GetExtension(file.FileName);
                return !options.Value.BlockedFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
            })
             .WithMessage("File extension is Blockd")
             .When(x => x is not null); ;
    }
}
