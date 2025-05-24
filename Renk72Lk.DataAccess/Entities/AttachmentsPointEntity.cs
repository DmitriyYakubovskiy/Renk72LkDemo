using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class AttachmentsPointEntity
{
    public int Id { get; set; }

    public float? Voltage { get; set; }
    public float? Power { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int? TechnicalSpecificationsId { get; set; }
    [JsonIgnore]
    public virtual BidTechnicalSpecificationsEntity TechnicalSpecifications { get; set; } = null!;
}
