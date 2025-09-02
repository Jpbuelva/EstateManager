namespace EstateManager.Domain.Entities;

public class Property
{
    public int IdProperty { get; set; }   // PK
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = string.Empty;
    public int Year { get; set; }

    // FK
    public int IdOwner { get; set; }
    public Owner? Owner { get; set; }

    // Navegación
    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
    public ICollection<PropertyTrace> Traces { get; set; } = new List<PropertyTrace>();
}
