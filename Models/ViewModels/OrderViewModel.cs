namespace Blazor_FE.Models.ViewModels;

public class OrderViewModel
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public int PromoId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal DiscountAmount { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; }
}

public class OrderItemViewModel
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal { get; set; }
    public ProductViewModel Product { get; set; }
}

public class ProductViewModel
{
    public string ProductName { get; set; }
    public string Barcode { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; }
    public string ProductImg { get; set; }
    public int SupplierId { get; set; }
}