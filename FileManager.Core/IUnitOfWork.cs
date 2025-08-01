using FileManager.Core.Entites;
using FileManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core;
public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<UploadedFile> FileRepository { get; }
    Task<int> CompleteAsync(CancellationToken cancellation = default);
   
}
