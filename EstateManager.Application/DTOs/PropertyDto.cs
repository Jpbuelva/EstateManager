namespace EstateManager.Application.DTOs;

public class PropertyDto
{
    public int IdProperty { get; set; }       
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Year { get; set; }
    public int IdOwner { get; set; }
}
