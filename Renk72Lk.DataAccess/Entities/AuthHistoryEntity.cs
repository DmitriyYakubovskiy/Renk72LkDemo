using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class AuthHistoryEntity
{
    public int Id { get; set; }
    public string LoginIp { get; set; } = null!;
    public DateTime LoginDateTime { get; set; }

    public DateTime CreatedAt { get;set; }  
    public DateTime UpdatedAt { get;set; }

    public int UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity User { get; set; }=null!;
}
