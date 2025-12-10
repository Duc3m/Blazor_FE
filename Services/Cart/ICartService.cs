using Blazor_FE.Models;

namespace Blazor_FE.Services.Cart
{
    public interface ICartService
    {
        /// Sự kiện được kích hoạt mỗi khi giỏ hàng thay đổi.
        event Action? OnCartChanged;

        /// Lấy tất cả các mặt hàng từ giỏ hàng (trong localStorage).
        Task<List<CartItemDTO>> GetCartItemsAsync();

        /// Thêm một sản phẩm vào giỏ hàng.
        /// Nếu sản phẩm đã tồn tại, tăng số lượng.
        Task AddToCartAsync(CartItemDTO item);

        /// Cập nhật số lượng của một mặt hàng.
        Task UpdateQuantityAsync(int productId, int quantity);

        /// Xóa một mặt hàng khỏi giỏ hàng dựa trên ProductId.
        Task RemoveFromCartAsync(int productId);

        /// Xóa toàn bộ giỏ hàng.
        Task ClearCartAsync();
    }
}
