using Renk72Lk.Models.DataBase;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models;

public interface IBaseUserData: IContactData, IPassportData
{
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Patronymic { get; set; }

    public string? Snils { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }

    [JsonIgnore]
    public AddressModel ActualAddress { get;}

    [JsonIgnore]
    public AddressModel RegistrationAddress { get;}
}
