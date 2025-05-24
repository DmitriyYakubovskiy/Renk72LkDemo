using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class BidPersonalInfoEntity
{
    public int Id { get; set; }

    public string? Surname { get; set; } 
    public string? Name { get; set; } 
    public string? Patronymic { get; set; } 
    public string? Snils { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }

    public string? PhoneNumber { get; set; } 
    public string? Email { get; set; } 

    public string? PassportType { get; set; } 
    public string? PassportSeries { get; set; } 
    public string? PassportNumber { get; set; } 
    public DateTime? PassportDate { get; set; } 
    public string? PassportIssuedBy { get; set; }

    public string? IsAgreePersonData { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? ActualAddressId { get; set; }
    [JsonIgnore]
    public virtual AddressEntity? ActualAddress { get; set; }
    
    public int? RegistrationAddressId { get; set; }
    [JsonIgnore]
    public virtual AddressEntity? RegistrationAddress { get; set; }
    
    public int? BidId { get; set; }
    [JsonIgnore]
    public virtual BidEntity? Bid { get; set; }
    
    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual UserEntity? User { get; set; }
}
