using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class BidTechnicalSpecificationsEntity
{
    public int Id { get; set; }

    public string? ReliabilityCategory { get; set; }

    public string? OldPointPower { get; set; }
    public string? OldPointVolt { get; set; }

    public string? CountOfTransformers { get; set; }
    public string? TransformersPower { get; set; }
    public string? CountOfGenerators { get; set; }
    public string? GeneratorsPower { get; set; }
    public string? TypeOfLoad { get; set; }
    public string? TechMin { get; set; }
    public string? JustificationTechMin { get; set; }

    public string? NatureLoad { get; set; }
    public string? PaymentOrder { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AttachmentsPointEntity> Points { get; set; } = new List<AttachmentsPointEntity>();
    public virtual ICollection<AttachmentsStageEntity> Stages { get; set; } = new List<AttachmentsStageEntity>();

    public int? BidId { get; set; }
    [JsonIgnore]
    public virtual BidEntity? Bid { get; set; }
    
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity? User { get; set; }
}
