using Renk72Lk.Attributes;
using System.Text.Json.Serialization;

namespace Renk72Lk.ViewModels;

[MessageViewModel]
public class MessageViewModel
{
    public int UserId { get; set; }
    public int BidId { get; set; }

    public string? Text { get; set; }

    [JsonIgnore]
    public List<IFormFile> DocFiles { get; set; } = new List<IFormFile>();
}
