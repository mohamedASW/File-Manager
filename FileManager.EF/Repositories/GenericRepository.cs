using FileManager.Core.Interfaces;
using FileManager.EF.Presistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.EF.Repositories;
public class GenericRepository<T>(ApplicationDbcontext context) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbcontext _context = context;

    public async Task AddAsync(T entity, CancellationToken cancellation = default)
    {
        await _context.Set<T>().AddAsync(entity,cancellation);
    }

    public async Task AddRangeAsync(IEnumerable<T> values, CancellationToken cancellation = default)
    {
        await _context.Set<T>().AddRangeAsync(values, cancellation);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        return await _context.FindAsync<T>([id], cancellation);
    }
}
