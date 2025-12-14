namespace Blazor_FE.Services.Products.dtos;

public class ProductFilterRequest
{
    public string? SearchTerm { get; set; } = "";

    public List<int>? CategoryIds { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public string? SortBy { get; set; } = "CreatedAt";
    public bool SortDescending { get; set; } = false;
}
