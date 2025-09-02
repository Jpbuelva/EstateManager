namespace EstateManager.Domain.Entities;

public class Property
{
    public int Id { get; set; }   // int Identity en SQL
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
}
