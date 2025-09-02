public class CreatePropertyDto
{
    
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Name { get; set; }

    public int Year { get; set; }
    public int IdOwner { get; set; }
    public string CodeInternal { get; set; } = string.Empty;
 
    public PropertyTraceDto? InitialTrace { get; set; }
}

public class PropertyTraceDto
{
    public DateTime DateSale { get; set; } = DateTime.UtcNow; 
    public decimal Value { get; set; }
    public decimal Tax { get; set; } = 0;
}
