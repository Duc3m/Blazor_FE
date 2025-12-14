namespace Blazor_FE.Services.Products.dtos;

public class ProductDetailResponseDTO
{
    public int ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public string Barcode { get; set; }
    
    public decimal Price { get; set; }
    
    public string Unit { get; set; }
    
    public string? ProductImg { get; set; }
    
    public string SupplierName { get; set; }
    
    public string CategoryName { get; set; }
}