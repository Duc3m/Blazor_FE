namespace Blazor_FE.Models;

public class InventoryDTO
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
