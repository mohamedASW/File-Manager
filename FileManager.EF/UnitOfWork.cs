using FileManager.Core;
using FileManager.Core.Interfaces;
using FileManager.EF.Presistance;
using FileManager.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.EF;
public class UnitOfWork(ApplicationDbcontext context, IServiceProvider serviceProvider) : IUnitOfWork
{
    private readonly ApplicationDbcontext _context = context;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private IFileRepository? _fileRepository;
    private static object _lock = new object();
    public IFileRepository FileRepository
    {
        get
        {
            if (_fileRepository is null)
            {

                lock (_lock)
                {
                    if (_fileRepository is null)
                        _fileRepository = _serviceProvider.GetRequiredService<IFileRepository>();
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
