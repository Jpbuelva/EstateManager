namespace EstateManager.Domain.Entities;

public class PropertyTrace
{
    public int IdPropertyTrace { get; set; }   // PK
    public DateTime DateSale { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }

    // FK
    public int IdProperty { get; set; }
    public Property? Property { get; set; }
}
