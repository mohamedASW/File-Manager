using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task AddAsync(T entity, CancellationToken cancellation = default);
    Task AddRangeAsync(IEnumerable<T> values , CancellationToken cancellation = default);
}
