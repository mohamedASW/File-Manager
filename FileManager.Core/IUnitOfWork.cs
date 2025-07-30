using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core;
public interface IUnitOfWork : IDisposable
{
    Task<int> CompleteAsync(CancellationToken cancellation = default);
   
}
