namespace EstateManager.Domain.Entities;

public class Owner
{
    public int IdOwner { get; set; }   // PK
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Photo { get; set; }  // Puede ser Base64 o URL
    public DateTime Birthday { get; set; }

    // Navegación
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
