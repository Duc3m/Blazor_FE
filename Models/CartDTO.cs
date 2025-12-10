namespace Blazor_FE.Models
{
    public class CartDTO
    {
        public string? UserId { get; set; }
        public decimal CartTotal { get; set; }

        public IEnumerable<CartItemDTO> CartItems { get; set; }
    }
}
