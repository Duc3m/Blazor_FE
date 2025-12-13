namespace Blazor_FE.Models.Product;

public class ProductResponseDTO
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Barcode { get; set; }

    public decimal Price { get; set; }

    public string? Unit { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ProductImg { get; set; }
}