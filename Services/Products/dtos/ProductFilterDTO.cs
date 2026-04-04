namespace Blazor_FE.Services.Products.dtos;

public class ProductFilterDTO
{
    public string? ProductName { get; set; }

    public string? Barcode { get; set; }

    public string? Unit { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ProductImg { get; set; }
    
    public string? Category { get; set; }

    public string? Supplier { get; set; }

    public string? SortOder { get; set; }

    public Decimal? MinPrice { get; set; }
    
    public Decimal? MaxPrice { get; set; }
    
    public DateOnly? StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }
}