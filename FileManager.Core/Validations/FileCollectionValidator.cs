using FileManager.Core.DTOs;
using FileManager.Core.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace FileManager.Core.Validations;

public class FileCollectionValidator : AbstractValidator<UploadFilesRequest>
{
    public FileCollectionValidator(IOptions<FileSettings> options)
    {
        RuleForEach(x => x.Files)
            .SetValidator(new FileSizeValidator(options))
            .SetValidator(new BlockedFileMimeTypeValidator(options))
            .SetValidator(new BlockedFileExtensionsValidator(options));

    }
}
