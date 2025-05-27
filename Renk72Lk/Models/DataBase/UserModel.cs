using Renk72Lk.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class UserModel:IBaseUserData
{
    public int Id { get; set; } = 0;

    [Required(ErrorMessage = "Не указано имя пользователя.")]
    public string? UserName { get; set; } = string.Empty;

    [NotEmptyMinStringLength(8, ErrorMessage = "Пароль должен быть длиннее 8 символов.")]
    [RequiredAlphaDigitPassword(ErrorMessage = "Пароль должен содержать цифры и буквы латинского алфавита.")]
    public string? Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указана почта.")]
    public string? Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указана фамилия.")]
    public string? Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указано имя.")]
    public string? Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указано отчество.")]
    public string? Patronymic { get; set; } = string.Empty;

    [NotEmptyMinStringLength(8)]
    [Required(ErrorMessage = "Не указан телефон.")]
    public string? PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Не указан паспорт.")]
    public string? PassportType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Обязательно для заполнения.")]
    public string? PassportSeries { get; set; } = string.Empty;

    [Required(ErrorMessage = "Обязательно для заполнения.")]
    public string? PassportNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Обязательно для заполнения.")]
    public DateTime? PassportDate { get; set; }

    [Required(ErrorMessage = "Обязательно для заполнения.")]
    public string? PassportIssuedBy { get; set; } = string.Empty;

    [NotEmptyMinStringLength(11)]
    //[Required(ErrorMessage = "Не указан СНИЛС.")]
    public string? Snils { get; set; } = null!;

    //[Required(ErrorMessage = "Обязательное поле.")]
    public DateTime? DateOfBirth { get; set; }

    //[Required(ErrorMessage = "Обязательное поле.")]
    public string? PlaceOfBirth { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? UserDataAgreementFileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel? UserDataAgreementFile { get; set; } = null!;
    
    public int? ActualAddressId { get; set; }
    [JsonIgnore]
    public AddressModel ActualAddress { get; set; } = null!;
    
    public int? RegistrationAddressId { get; set; }
    [JsonIgnore]
    public AddressModel RegistrationAddress { get; set; } = null!;
    
    [JsonIgnore]
    public List<AuthHistoryModel> Stories { get; set; } = new List<AuthHistoryModel>();
    
    [JsonIgnore]
    public List<BidModel> Bids { get; set; } = new List<BidModel>();
    
    [JsonIgnore]
    public List<MessageModel> Messages { get; set; } = new List<MessageModel>();

    [JsonIgnore]
    public List<BidPersonalInfoModel> PersonalInfo { get; set; } = new List<BidPersonalInfoModel>();
    
    [JsonIgnore]
    public List<BidRepresentativeInfoModel> RepresentativeInfo { get; set; } = new List<BidRepresentativeInfoModel>();
    
    [JsonIgnore]
    public List<BidConnectionObjectInfoModel> ConnectionObjectInfo { get; set; } = new List<BidConnectionObjectInfoModel>();
    
    [JsonIgnore]
    public List<BidTechnicalSpecificationsModel> TechnicalSpecifications { get; set; } = new List<BidTechnicalSpecificationsModel>();
    
    [JsonIgnore]
    public List<BidAttachmentsModel> Attachments { get; set; } = new List<BidAttachmentsModel>();
}
