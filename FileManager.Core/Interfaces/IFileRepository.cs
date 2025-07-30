using FileManager.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core.Interfaces;
public interface IFileRepository
{
    Task  SaveFileAsync(UploadedFile file, CancellationToken cancellation = default); 
}
