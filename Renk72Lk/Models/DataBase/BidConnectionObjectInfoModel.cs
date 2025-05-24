using Renk72Lk.Attributes;
using Renk72Lk.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class BidConnectionObjectInfoModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Не указана область.")]
    public string? Region { get; set; }

    [Required(ErrorMessage = "Не указан район.")]
    public string? District { get; set; }

    [NotEmptyMinStringLength(5)]
    [Required(ErrorMessage = "Не указан адрес.")]
    public string? AddressOfObject { get; set; }

    [NotEmptyMinStringLength(10)]
    [Required(ErrorMessage = "Не указан кадастровый номер.")]
    public string? CadastralNumber { get; set; }

    [Required(ErrorMessage = "Не указана причина обращения.")]
    public string? ReasonForBid { get; set; }

    [Required(ErrorMessage = "Не указан поставщик.")]
    public string? GuarantySupplier { get; set; }

    [Required(ErrorMessage = "Не указан вид договора.")]
    public string? TypeOfContract { get; set; }

    [Required(ErrorMessage = "Не указан класс напряжения.")]
    public string? VoltageClass { get; set; }

    [Required(ErrorMessage = "Не указан характер присоединения.")]
    public string? ConnectionType { get; set; }

    public string? PowerDevice { get; set; }

    [Required(ErrorMessage = "Не указано наименование объекта.")]
    public string? ObjectName { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string? Role { get; set; }

    public int? BidId { get; set; }

    public int? UserId { get; set; }
    [JsonIgnore]
    public UserEntity? User { get; set; }
}
