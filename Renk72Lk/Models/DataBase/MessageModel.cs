using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class MessageModel
{
    public int Id { get; set; }

    public string? Message { get; set; }
    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? FileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel File { get; set; } = null!;

    public int? BidId { get; set; }
    [JsonIgnore]
    public BidModel Bid { get; set; } = null!;

    public int? UserId { get; set; }
    [JsonIgnore]
    public UserModel User { get; set; } = null!;
}
