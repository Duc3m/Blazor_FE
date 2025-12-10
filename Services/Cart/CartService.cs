using Blazored.LocalStorage;
using Blazor_FE.Models;

namespace Blazor_FE.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private const string CartStorageKey = "user_cart";

        public event Action? OnCartChanged;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<CartItemDTO>> GetCartItemsAsync()
        {
            return await _localStorage.GetItemAsync<List<CartItemDTO>>(CartStorageKey) ?? new List<CartItemDTO>();
        }

        public async Task AddToCartAsync(CartItemDTO item)
        {
            var cart = await GetCartItemsAsync();
            var existingItem = cart.FirstOrDefault(cartItem => cartItem.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            } else
            {
                // Thêm mặt hàng mới vào giỏ hàng
                cart.Add(item);
            }
            
            await _localStorage.SetItemAsync(CartStorageKey, cart);
            OnCartChanged?.Invoke();
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            var cart = await GetCartItemsAsync();
            var itemToUpdate = cart.FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (itemToUpdate != null)
            {
                if (quantity > 0)
                {
                    itemToUpdate.Quantity = quantity;
                }
                else
                {
                    // Nếu số lượng là 0 hoặc ít hơn, xóa mặt hàng
                    cart.Remove(itemToUpdate);
                }

                await _localStorage.SetItemAsync(CartStorageKey, cart);
                OnCartChanged?.Invoke();
            }
        }

        public async Task RemoveFromCartAsync(int productId)
        {
            var cart = await GetCartItemsAsync();
            var itemToRemove = cart.FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                await _localStorage.SetItemAsync(CartStorageKey, cart);
                OnCartChanged?.Invoke();
            }
        }

        public async Task ClearCartAsync()
        {
            await _localStorage.RemoveItemAsync(CartStorageKey);
            OnCartChanged?.Invoke();
        }
    }
}
