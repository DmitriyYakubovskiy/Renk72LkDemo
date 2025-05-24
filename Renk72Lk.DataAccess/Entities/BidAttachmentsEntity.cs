using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class BidAttachmentsEntity
{
    public int Id { get; set; }

    public string? IsAgreePreviewPDF { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? PassportFileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? PassportFile { get; set; }
    
    public int? PowerDevicesPlanFileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? PowerDevicesPlanFile { get; set; }
    
    public int? SnilsFileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? SnilsFile { get; set; }
    
    public int? BenefitFileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? BenefitFile { get; set; }

    public int? OtherFileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? OtherFile { get; set; }
    
    public int? BidId { get; set; }
    [JsonIgnore]
    public virtual BidEntity? Bid { get; set; }
    
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity? User { get; set; }
}
