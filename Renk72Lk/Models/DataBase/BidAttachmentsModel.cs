using Renk72Lk.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Renk72Lk.Models.DataBase;

public class BidAttachmentsModel
{
    public int Id { get; set; }

    [MustBeTrue(ErrorMessage = "Необходимо согласие.")]
    [Required(ErrorMessage = "Обязательное поле.")]
    public string? IsAgreePreviewPDF { get; set; }
    public string? Role { get; set; }

    public string IsAttorney { get; set; } = "off";
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? PassportFileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel? PassportFile { get; set; }

    public int? PowerDevicesPlanFileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel? PowerDevicesPlanFile { get; set; }
    
    public int? SnilsFileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel? SnilsFile { get; set; }
    
    public int? BenefitFileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel? BenefitFile { get; set; }
    
    public int? OtherFileId { get; set; }
    [JsonIgnore]
    public AttachmentFileModel? OtherFile { get; set; }
    
    public int? BidId { get; set; }

    public int? UserId { get; set; }
    [JsonIgnore]
    public UserModel? User { get; set; }

    public IFormFile[]? OtherFiles { get; set; }
    public IFormFile[]? PassportFiles { get; set; }
    public IFormFile[]? SnilsFiles { get; set; }
    public IFormFile[]? PlanFiles { get; set; }
    public IFormFile[]? BenefitFiles { get; set; }
}
