namespace Renk72Lk.Models.DataBase;

public class AuthHistoryModel
{
    public int Id { get; set; }
    public string? LoginIp { get; set; }
    public DateTime? LoginDateTime { get; set; }

    public int? UserId { get; set; }
}
