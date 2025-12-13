namespace Blazor_FE.Components.Pages.Product;

public class ProductSearchDTO
{
    public string? ProductName { get; set; }

    public string? Barcode { get; set; }

    public string? Unit { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ProductImg { get; set; }
    
    public string? Category { get; set; }

    public string? Supplier { get; set; }

    public String? SortOder { get; set; }

    public Decimal? MinPrice { get; set; }
    
    public Decimal? MaxPrice { get; set; }
    
    public DateOnly? StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }
}