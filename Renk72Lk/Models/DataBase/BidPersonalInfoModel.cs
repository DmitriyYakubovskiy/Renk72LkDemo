using Renk72Lk.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class BidPersonalInfoModel: IBaseUserData
{
    public int Id { get; set; }

    [NotEmptyMinStringLength(2)]
    [Required(ErrorMessage = "Не указана фамилия.")]
    public string? Surname { get; set; }

    [NotEmptyMinStringLength(2)]
    [Required(ErrorMessage = "Не указано имя.")]
    public string? Name { get; set; }

    [NotEmptyMinStringLength(2)]
    [Required(ErrorMessage = "Не указано отчество.")]
    public string? Patronymic { get; set; }

    [NotEmptyMinStringLength(11)]
    [RequiredIf("Role", new[] { "Физическое лицо" }, ErrorMessage = "Не указан СНИЛС.")]
    public string? Snils { get; set; }

    [NotEmptyMinStringLength(2)]
    [RequiredIf("Role", new[] { "Физическое лицо" }, ErrorMessage = "Не указана дата рождения.")]
    public DateTime? DateOfBirth { get; set; }

    [NotEmptyMinStringLength(2)]
    [RequiredIf("Role", new[] { "Физическое лицо" }, ErrorMessage = "Не указано место рождения.")]
    public string? PlaceOfBirth { get; set; }

    [NotEmptyMinStringLength(11)]
    [Required(ErrorMessage = "Не указан номер телефона.")]
    public string? PhoneNumber { get; set; }

    [NotEmptyMinStringLength(4)]
    [Required(ErrorMessage = "Не указана почта.")]
    public string? Email { get; set; }

    [MustBeTrue(ErrorMessage = "Необходимо согласие.")]
    [RequiredIf("Role", new[] { "Физическое лицо" })]
    public string? IsAgreePersonData { get; set; }

    [RequiredIf("Role", new[] { "Физическое лицо", "Индивидуальный предприниматель" }, ErrorMessage = "Не указан паспорт.")]
    public string? PassportType { get; set; }

    [NotEmptyMinStringLength(4)]
    [RequiredIf("Role", new[] { "Физическое лицо", "Индивидуальный предприниматель" }, ErrorMessage = "Не указана серия паспорта.")]
    public string? PassportSeries { get; set; }

    [NotEmptyMinStringLength(4)]
    [RequiredIf("Role", new[] { "Физическое лицо", "Индивидуальный предприниматель" }, ErrorMessage = "Не указан номер паспорта.")]
    public string? PassportNumber { get; set; }

    [NotEmptyMinStringLength(8)]
    [RequiredIf("Role", new[] { "Физическое лицо", "Индивидуальный предприниматель" }, ErrorMessage = "Не указана дата выдачи паспорта.")]
    public DateTime? PassportDate { get; set; }

    [NotEmptyMinStringLength(3)]
    [RequiredIf("Role", new[] { "Физическое лицо", "Индивидуальный предприниматель" }, ErrorMessage = "пппп.")]
    public string? PassportIssuedBy { get; set; }

    public DateTime? CreatedAt { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; } = null!;

    public string? Role { get; set; }

    public int? ActualAddressId { get; set; }
    [JsonIgnore]
    public AddressModel? ActualAddress { get; set; }
    
    public int? RegistrationAddressId { get; set; }
    [JsonIgnore]
    public AddressModel? RegistrationAddress { get; set; }

    public int? BidId { get; set; }
    
    public int? UserId { get; set; }
    [JsonIgnore]
    public UserModel? User { get; set; }
}
