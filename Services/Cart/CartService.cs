using Blazor_FE.Extensions;
using Blazor_FE.Models;
using Blazor_FE.Services.Auth;
using Blazored.LocalStorage;

namespace Blazor_FE.Services.Cart;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IUserContextService _userContextService;
    private List<CartItemModel>? _cartCache = null;

    public event Action? OnCartChanged;

    public CartService(ILocalStorageService localStorage, IUserContextService userContextService)
    {
        _localStorage = localStorage;
        _userContextService = userContextService;
    }

    private async Task<string> GetCartStorageKeyAsync()
    {
        int userId = await _userContextService.GetUserIdAsync();
        return userId != 0 ? $"cart_{userId}" : "guest_cart";
    }

    public async Task LoadCartFromStorageAsync()
    {
        var storageKey = await GetCartStorageKeyAsync();
        _cartCache = await _localStorage.SafeGetItemAsync<List<CartItemModel>>(storageKey) 
            ?? new List<CartItemModel>();
        OnCartChanged?.Invoke();
    }

    public async Task<List<CartItemModel>> GetCartItemsAsync()
    {
        if (_cartCache == null)
        {
            await LoadCartFromStorageAsync();
        }
        return _cartCache;
    }

    public async Task AddToCartAsync(CartItemModel item)
    {
        if (_cartCache == null) await LoadCartFromStorageAsync();
        var existingItem = _cartCache.FirstOrDefault(cartItem => cartItem.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            _cartCache.Add(item);
        }

        await SaveAndNotifyAsync();
    }

    public async Task UpdateQuantityAsync(int productId, int quantity)
    {
        var itemToUpdate = _cartCache.FirstOrDefault(cartItem => cartItem.ProductId == productId);
        if (itemToUpdate == null) return;
        if (quantity > 0)
        {
            itemToUpdate.Quantity = quantity;
        }
        else
        {
            _cartCache.Remove(itemToUpdate);
        }
        await SaveAndNotifyAsync();
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var itemToRemove = _cartCache.FirstOrDefault(cartItem => cartItem.ProductId == productId);
        if (itemToRemove == null) return;
        _cartCache.Remove(itemToRemove);
        await SaveAndNotifyAsync();
    }

    public async Task ClearCartAsync()
    {
        _cartCache.Clear();
        var storageKey = await GetCartStorageKeyAsync();
        try
        {
            await _localStorage.RemoveItemAsync(storageKey);
        }
        catch (InvalidOperationException) { }
        OnCartChanged?.Invoke();
    }

    private async Task SaveAndNotifyAsync()
    {
        var storageKey = await GetCartStorageKeyAsync();
        await _localStorage.SafeSetItemAsync(storageKey, _cartCache);
        OnCartChanged?.Invoke();
    }

}
