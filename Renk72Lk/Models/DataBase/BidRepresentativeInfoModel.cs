using Renk72Lk.Attributes;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class BidRepresentativeInfoModel  : IPassportData, IContactData
{
    public int Id { get; set; }
    [RequiredIf("IsAttorney", new[] { "on" })]
    public string? Surname { get; set; }

    [RequiredIf("IsAttorney", new[] { "on" })]
    public string? Name { get; set; }
    public string? Patronymic { get; set; }
    public string? Snils { get; set; }
    public string? Attorney { get; set; }

    [RequiredIf("IsAttorney", new[] { "on" })]
    public string? PhoneNumber { get; set; }

    [RequiredIf("IsAttorney", new[] { "on" })]
    public string? Email { get; set; }

    public string? PassportType { get; set; }
    public string? PassportSeries { get; set; }
    public string? PassportNumber { get; set; }
    public DateTime? PassportDate { get; set; }
    public string? PassportIssuedBy { get; set; }

    public string? IsAttorney { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Role { get; set; }

    public int? BidId { get; set; }

    public int? UserId { get; set; }
    [JsonIgnore]
    public UserModel? User { get; set; }
}
