using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Microsoft.AspNetCore.WebUtilities;

namespace Blazor_FE.Services.Supplier;

public class SupplierService : ISupplierService
{
    private readonly HttpClient _httpClient;

    public SupplierService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<APIResponse<T>> GetAllAsync<T>()
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = 1.ToString(),
                ["pageSize"] = 12.ToString()
            };

            var url = QueryHelpers.AddQueryString("api/v1/suppliers", queryParams);

            var response = await _httpClient.GetFromJsonAsync<APIResponse<T>>(url);
            if (response == null)
            {
                throw new Exception("Khong the goi supplier");
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task<T?> AddAsync<T>()
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = 1.ToString(),
                ["pageSize"] = 12.ToString()
            };

            var url = QueryHelpers.AddQueryString("api/v1/suppliers", queryParams);

            var response = _httpClient.GetFromJsonAsync<T>(url);
            return await response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}