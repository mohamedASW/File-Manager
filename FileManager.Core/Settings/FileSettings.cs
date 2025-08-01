using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core.Settings;

public class FileSettings
{
    public int MaxFileSize { get; set; }
    public string[] AllowedImageExtensions { get; set; } = [];
    public string[] BlockedFileExtensions { get; set; } = [];
    
    public string[] BlockedMimeTypes { get; set; } = [];
}

