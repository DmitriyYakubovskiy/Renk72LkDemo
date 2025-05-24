namespace Renk72Lk.Models;

public interface IPassportData
{
    public string? PassportType { get; set; }
    public string? PassportSeries { get; set; }
    public string? PassportNumber { get; set; }
    public DateTime? PassportDate { get; set; }
    public string? PassportIssuedBy { get; set; }
}
