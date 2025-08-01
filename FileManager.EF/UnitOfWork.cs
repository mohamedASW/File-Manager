using FileManager.Core;
using FileManager.Core.Entites;
using FileManager.Core.Interfaces;
using FileManager.EF.Presistance;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.EF;
public class UnitOfWork(ApplicationDbcontext context, IServiceProvider serviceProvider) : IUnitOfWork
{
    private readonly ApplicationDbcontext _context = context;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private IGenericRepository<UploadedFile>? _fileRepository;
    private static object _lock = new object();
    public IGenericRepository<UploadedFile> FileRepository
    {
        get
        {
            if (_fileRepository is null)
            {

                lock (_lock)
                {
                    if (_fileRepository is null)
                        _fileRepository = _serviceProvider.GetRequiredService<IGenericRepository<UploadedFile>>();
                }
            }
            return _fileRepository;
        }
    }
    public Task<int> CompleteAsync(CancellationToken cancellation = default)
    {
        return _context.SaveChangesAsync(cancellation);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
