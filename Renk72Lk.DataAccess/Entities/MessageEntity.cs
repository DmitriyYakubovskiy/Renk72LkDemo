using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class MessageEntity
{
    public int Id { get; set; }

    public string? Message { get; set; }
    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? FileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? File { get; set; }
    
    public int? BidId { get; set; }
    [JsonIgnore]
    public virtual BidEntity? Bid { get; set; }
    
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity? User { get; set; }
}
