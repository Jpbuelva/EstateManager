namespace EstateManager.Domain.Entities;

public class PropertyImage
{
    public int IdPropertyImage { get; set; }   // PK

    // FK
    public int IdProperty { get; set; }
    public Property? Property { get; set; }

    // Archivo (Base64 o URL)
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}
