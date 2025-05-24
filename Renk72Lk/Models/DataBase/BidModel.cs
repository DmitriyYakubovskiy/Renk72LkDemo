using Microsoft.AspNetCore.Mvc;
using Renk72Lk.DataAccess.Entities;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class BidModel
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? UserRole { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Service { get; set; }

    public int? Status { get; set; }
    public int? TicketStatus { get; set; }
    public int? IsArchive { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? DocumentFileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel? DocumentFile { get; set; }

    [JsonIgnore]
    public List<MessageEntity> Messages { get; set; } = new List<MessageEntity>();
    
    [JsonIgnore]
    public UserModel? User { get; set; }

    public BidPersonalInfoModel? Step1 { get; set; }

    public BidRepresentativeInfoModel? Step2 { get; set; }

    public BidConnectionObjectInfoModel? Step3 { get; set; }

    public BidTechnicalSpecificationsModel? Step4 { get; set; }

    public BidAttachmentsModel? Step5 { get; set; }
}
