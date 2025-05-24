namespace Renk72Lk.Models.DataBase;

public class AddressModel
{
    public int Id { get; set; }
    public string? Index { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Build { get; set; }
    public string? Office { get; set; }
    public string? FullAddress
    {
        get
        {
            var parts = new List<string>();

            if (!string.IsNullOrEmpty(Region)) parts.Add(Region);
            if (!string.IsNullOrEmpty(City)) parts.Add(City);
            if (!string.IsNullOrEmpty(Street)) parts.Add(Street);

            var housePart = !string.IsNullOrEmpty(House) ? (!string.IsNullOrEmpty(Office) ? $"{House} {Office}" : House): null;

            if (!string.IsNullOrEmpty(housePart)) parts.Add(housePart);
            if (!string.IsNullOrEmpty(Index)) parts.Add(Index);

            return parts.Count > 0 ? string.Join(", ", parts) : null;
        }
    }
}
