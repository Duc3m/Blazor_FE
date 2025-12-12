namespace Blazor_FE.Models
{
    public class CartModel
    {
        public string? UserId { get; set; }
        public decimal CartTotal { get; set; }

        public IEnumerable<CartItemModel> CartItems { get; set; }
    }
}
