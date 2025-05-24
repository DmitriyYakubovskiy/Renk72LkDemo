namespace Renk72Lk.DataAccess.Entities;

public class AttachmentFileEntity
{
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
