using Blazor_FE.Models;

namespace Blazor_FE.Services.Cart
{
    public interface ICartService
    {
        /// Sự kiện được kích hoạt mỗi khi giỏ hàng thay đổi.
        event Action? OnCartChanged;
        Task<List<CartItemDTO>> GetCartItemsAsync();
        Task AddToCartAsync(CartItemDTO item);

        Task UpdateQuantityAsync(int productId, int quantity);
        Task RemoveFromCartAsync(int productId);
        Task ClearCartAsync();
        Task LoadCartFromStorageAsync();
    }
}
