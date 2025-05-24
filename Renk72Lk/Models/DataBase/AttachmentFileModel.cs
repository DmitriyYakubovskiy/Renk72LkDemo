namespace Renk72Lk.Models.DataBase;

public class AttachmentFileModel
{
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public IFormFile? FormFile { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
