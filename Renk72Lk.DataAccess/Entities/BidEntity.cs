using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class BidEntity
{
    public int Id { get; set; }

    public string? UserRole { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Service { get; set; }

    public int? Status { get; set; }
    public int? TicketStatus { get; set; } 
    public int? IsArchive { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? DocumentFileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? DocumentFile { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<MessageEntity> Messages { get; set; } = new List<MessageEntity>();
    
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity? User { get; set; }

    [JsonIgnore]
    public virtual BidPersonalInfoEntity? Step1 { get; set; }

    [JsonIgnore]
    public virtual BidRepresentativeInfoEntity? Step2 { get; set; }

    [JsonIgnore]
    public virtual BidConnectionObjectInfoEntity? Step3 { get; set; }

    [JsonIgnore]
    public virtual BidTechnicalSpecificationsEntity? Step4 { get; set; }

    [JsonIgnore]
    public virtual BidAttachmentsEntity? Step5 { get; set; }
}
