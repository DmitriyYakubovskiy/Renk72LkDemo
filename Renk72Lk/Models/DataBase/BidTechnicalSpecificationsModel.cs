using Renk72Lk.Attributes;
using Renk72Lk.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class BidTechnicalSpecificationsModel
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int? BidId { get; set; }

    [Required(ErrorMessage = "Обязательное поле.")]
    public string? ReliabilityCategory { get; set; }

    public float? OldPointPower { get; set; }
    public float? OldPointVolt { get; set; }

    public int? CountOfTransformers { get; set; }
    public float? TransformersPower { get; set; }
    public int? CountOfGenerators { get; set; }
    public float? GeneratorsPower { get; set; }
    [RequiredIf("Service", new[] { "Технологическое присоединение энергопринимающих устройств до 150 кВт" })]
    public string? TypeOfLoad { get; set; }
    public float? TechMin { get; set; }
    public string? JustificationTechMin { get; set; }

    [RequiredIf("Service", new[] { "Технологическое присоединение энергопринимающих устройств до 150 кВт" })]
    public string? NatureLoad { get; set; }

    [RequiredIf("Service", new[] { "Технологическое присоединение энергопринимающих устройств до 150 кВт" })]
    public string? PaymentOrder { get; set; }

    public string? Reason { get; set; }
    public string? Role { get; set; }
    public string? Service { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<AttachmentsPointEntity> Points { get; set; } = new List<AttachmentsPointEntity>();
    
    public List<AttachmentsStageEntity> Stages { get; set; } = new List<AttachmentsStageEntity>();

    [JsonIgnore]
    public UserModel? User { get; set; }
}
