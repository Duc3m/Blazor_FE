namespace Blazor_FE.Services.Products.dtos;

public class ProductRequestDTO
{
    public int? SupplierId { get; set; }
    public int? CategoryId { get; set; }
    
    public string ProductName { get; set; } = null!;
    public string Barcode { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; }
    public string? ProductImg { get; set; }
}