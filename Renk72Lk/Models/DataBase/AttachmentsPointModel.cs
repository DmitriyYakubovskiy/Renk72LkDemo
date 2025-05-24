using Renk72Lk.Models.DataBase;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models;

public class AttachmentsPointModel
{
    public int Id { get; set; }

    public float? Voltage { get; set; }
    public float? Power { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int TechnicalSpecificationsId { get; set; }
    [JsonIgnore]
    public BidTechnicalSpecificationsModel? TechnicalSpecifications { get; set; }
}
