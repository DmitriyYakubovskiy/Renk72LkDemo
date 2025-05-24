namespace Renk72Lk.DataAccess.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string? Index { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Build { get; set; }
    public string? Office { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
