using System.ComponentModel.DataAnnotations;

namespace Blazor_FE.Models.Dtos;

public class OrderDTO
{
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public string? PromoCode { get; set; }

    public string PaymentMethod { get; set; } = "bank_transfer";

    public string? Note { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất 1 sản phẩm")]
    public List<OrderItemDTO> Items { get; set; } = new();
}

public class OrderItemDTO
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}