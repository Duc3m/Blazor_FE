using Blazor_FE.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blazor_FE.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IServiceProvider _serviceProvider;
        private List<CartItemDTO> _cartCache = new(); // Sử dụng cache để giảm truy cập LocalStorage


        public event Action? OnCartChanged;

        public CartService(ILocalStorageService localStorage, IServiceProvider serviceProvider)
        {
            _localStorage = localStorage;
            _serviceProvider = serviceProvider;
        }

        // Phương thức mới để xác định key lưu trữ dựa trên trạng thái đăng nhập
        private async Task<string> GetCartStorageKeyAsync()
        {
            var authStateProvider = _serviceProvider.GetRequiredService<AuthenticationStateProvider>();
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated ?? false)
            {
                // Nếu người dùng đã đăng nhập, sử dụng UserId làm key
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return $"cart_{userId}";
            }
            else
            {
                // Nếu là khách, sử dụng một key chung
                return "guest_cart";
            }
        }

        // Phương thức mới để tải giỏ hàng vào cache
        public async Task LoadCartFromStorageAsync()
        {
            var storageKey = await GetCartStorageKeyAsync();
            _cartCache = await _localStorage.GetItemAsync<List<CartItemDTO>>(storageKey) ?? new List<CartItemDTO>();
            OnCartChanged?.Invoke();
        }

        public async Task AddToCartAsync(CartItemDTO item)
        {
            var existingItem = _cartCache.FirstOrDefault(cartItem => cartItem.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cartCache.Add(item);
            }

            var storageKey = await GetCartStorageKeyAsync();
            await _localStorage.SetItemAsync(storageKey, _cartCache);
            OnCartChanged?.Invoke();
        }

        public Task<List<CartItemDTO>> GetCartItemsAsync()
        {
            // Trả về từ cache thay vì đọc trực tiếp từ LocalStorage
            return Task.FromResult(_cartCache);
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            var itemToUpdate = _cartCache.FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (itemToUpdate != null)
            {
                if (quantity > 0)
                {
                    itemToUpdate.Quantity = quantity;
                }
                else
                {
                    _cartCache.Remove(itemToUpdate);
                }

                var storageKey = await GetCartStorageKeyAsync();
                await _localStorage.SetItemAsync(storageKey, _cartCache);
                OnCartChanged?.Invoke();
            }
        }

        public async Task RemoveFromCartAsync(int productId)
        {
            var itemToRemove = _cartCache.FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (itemToRemove != null)
            {
                _cartCache.Remove(itemToRemove);
                var storageKey = await GetCartStorageKeyAsync();
                await _localStorage.SetItemAsync(storageKey, _cartCache);
                OnCartChanged?.Invoke();
            }
        }

        public async Task ClearCartAsync()
        {
            _cartCache.Clear();
            var storageKey = await GetCartStorageKeyAsync();
            await _localStorage.RemoveItemAsync(storageKey);
            OnCartChanged?.Invoke();
        }
    }
}
