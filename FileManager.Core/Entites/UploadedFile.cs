namespace FileManager.Core.Entites;

public class UploadedFile
{

    public Guid Id { get; private set; }
    public UploadedFile()
    {
        Id = Guid.CreateVersion7();
    }
    public string FileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
}