using FileManager.Core.Entites;
using FileManager.Core.Interfaces;
using FileManager.EF.Presistance;

namespace FileManager.EF.Repositories;
public class FileRepository(ApplicationDbcontext context) : IFileRepository
{
    private readonly ApplicationDbcontext _context = context;

    public async Task SaveFileAsync(UploadedFile file, CancellationToken cancellation = default)
    {
        await _context.Files.AddAsync(file);
    }
}
