using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Renk72Lk.DataAccess.Entities;

public class UserEntity : IdentityUser<int>
{
    //public int Id { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }   
    public string? Patronymic { get; set; }
    //public string? Login { get; set; } UserName
    //public string? Password { get; set; } PasswordHash
    //public string? Phone { get;set; }  PhoneNumber
    //public string? Email { get; set; }
    public string? Snils { get; set; }
    
    public string? PassportType {  get; set; } 
    public string? PassportSeries { get; set; }   
    public string? PassportNumber { get; set; }
    public DateTime? PassportDate { get; set; }
    public string? PassportIssuedBy { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? UserDataAgreementFileId { get; set; }
    [JsonIgnore]
    public virtual AttachmentFileEntity? UserDataAgreementFile { get; set; }

    public int? ActualAddressId { get; set; }
    [JsonIgnore]
    public virtual AddressEntity? ActualAddress { get; set; }
    
    public int? RegistrationAddressId { get; set; }
    [JsonIgnore]
    public virtual AddressEntity? RegistrationAddress { get; set; }

    [JsonIgnore]
    public virtual ICollection<AuthHistoryEntity> AuthHistory { get; set; } = new List<AuthHistoryEntity>();
    [JsonIgnore]
    public virtual ICollection<BidEntity> Bids { get; set; } = new List<BidEntity>();
    [JsonIgnore]
    public virtual ICollection<MessageEntity> Messages { get; set; } = new List<MessageEntity>();

    [JsonIgnore]
    public virtual ICollection<BidPersonalInfoEntity> PersonalInfos { get; set; } = new List<BidPersonalInfoEntity>();
    [JsonIgnore]
    public virtual ICollection<BidRepresentativeInfoEntity> RepresentativeInfos { get; set; } = new List<BidRepresentativeInfoEntity>();
    [JsonIgnore]
    public virtual ICollection<BidConnectionObjectInfoEntity> ConnectionObjectInfos { get; set; } = new List<BidConnectionObjectInfoEntity>();
    [JsonIgnore]
    public virtual ICollection<BidTechnicalSpecificationsEntity> TechnicalSpecifications { get; set; } = new List<BidTechnicalSpecificationsEntity>();
    [JsonIgnore]
    public virtual ICollection<BidAttachmentsEntity> Attachments { get; set; } = new List<BidAttachmentsEntity>();
}
