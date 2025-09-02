namespace EstateManager.Domain.Entities;

public class PropertyImage
{
    public int Id { get; set; }   // int Identity en SQL
    public string Url { get; set; } = string.Empty;
    public int PropertyId { get; set; }

    public Property? Property { get; set; }
}
