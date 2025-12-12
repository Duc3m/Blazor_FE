using Blazored.LocalStorage;

namespace Blazor_FE.Extensions;

public static class LocalStorageExtensions
{

    public static async Task<T?> SafeGetItemAsync<T>(this ILocalStorageService localStorageService,  string key)
    {
        try
        {
            return await localStorageService.GetItemAsync<T>(key);
        }
        catch (InvalidOperationException)
        {
            return default;
        }
    }

    public static async Task SafeSetItemAsync<T>(this ILocalStorageService localStorageService, string key, T data)
    {
        try
        {
            await localStorageService.SetItemAsync(key, data);
        }
        catch (InvalidOperationException) { /* Bỏ qua lỗi prerender */ }
    }

}
