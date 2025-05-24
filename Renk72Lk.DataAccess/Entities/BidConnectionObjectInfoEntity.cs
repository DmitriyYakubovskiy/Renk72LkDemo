using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class BidConnectionObjectInfoEntity
{
    public int Id { get; set; }

    public string? Region { get; set; }
    public string? District { get; set; }
    public string? AddressOfObject { get; set; }
    public string? CadastralNumber { get; set; }

    public string? ReasonForBid { get; set; }

    public string? GuarantySupplier { get; set; }

    public string? TypeOfContract { get; set; }
    
    public string? VoltageClass { get; set; }
    public string? ConnectionType { get; set; }
    public string? PowerDevice { get; set; }
    public string? ObjectName { get; set; }

    public DateTime? CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }

    public int? BidId { get; set; }
    [JsonIgnore]
    public virtual BidEntity? Bid { get; set; }
    
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity? User { get; set; }
}
