using FileManager.Core.DTOs;
using FileManager.Core.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace FileManager.Core.Validations;

public class ImageFileValidator : AbstractValidator<UploadImagesRequest>
{
    public ImageFileValidator(IOptions<FileSettings> options)
    {
        RuleFor(x => x.Image)
            .Must(image =>
            {
                var imgeExtension = Path.GetExtension(image.FileName);
                return options.Value.AllowedImageExtensions.Contains(imgeExtension, StringComparer.OrdinalIgnoreCase);
            })
            .WithMessage("File extension is not allowed")
            .When(x => x.Image is not null)
            .SetValidator(new FileSizeValidator(options))
            .SetValidator(new BlockedFileMimeTypeValidator(options));

    }
}
