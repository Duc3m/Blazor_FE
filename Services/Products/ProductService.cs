using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Microsoft.AspNetCore.WebUtilities;


namespace Blazor_FE.Services.Products;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<APIResponse<List<ProductModel>>> GetProductsAsync()
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = 1.ToString(),
                ["pageSize"] = 12.ToString()
            };

            var url = QueryHelpers.AddQueryString("api/v1/product", queryParams);

            var response = _httpClient.GetFromJsonAsync<APIResponse<List<ProductModel>>>(url);
            return await response;
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<List<ProductModel>>> GetProductsPageAsync(int page, int pageSize)
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = page.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            var url = QueryHelpers.AddQueryString("api/v1/product", queryParams);

            var response = _httpClient.GetFromJsonAsync<APIResponse<List<ProductModel>>>(url);
            return await response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
