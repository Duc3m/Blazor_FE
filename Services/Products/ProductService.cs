using Blazor_FE.Models;
using Blazor_FE.Models.Product;
using Blazor_FE.Services.Products.dtos;
using Blazor_FE.Services.Base;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;


namespace Blazor_FE.Services.Products;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(
        HttpClient httpClient)
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
        catch (Exception ex)
        {
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

    public async Task<APIResponse<List<ProductModel>>> FilterProductAsync(ProductFilterRequest filterRequest, int page, int pageSize)
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = page.ToString(),
                ["pageSize"] = pageSize.ToString(),
                ["searchTerm"] = filterRequest.SearchTerm,
                ["minPrice"] = filterRequest.MinPrice?.ToString(),
                ["maxPrice"] = filterRequest.MaxPrice?.ToString(),
                ["sortBy"] = filterRequest.SortBy,
                ["sortDescending"] = filterRequest.SortDescending.ToString() 
            };

            var url = QueryHelpers.AddQueryString("api/v1/product", queryParams);
            if (filterRequest.CategoryIds != null && filterRequest.CategoryIds.Any())
            {
                foreach (var id in filterRequest.CategoryIds)
                {
                    url = QueryHelpers.AddQueryString(url, "categoryIds", id.ToString());
                }
            }

            var response = _httpClient.GetFromJsonAsync<APIResponse<List<ProductModel>>>(url);
            return await response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> GetProductsPageAsyncV1<T>(int page, int pageSize) where T : class
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = page.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            var url = QueryHelpers.AddQueryString("api/v1/product", queryParams);
            var res = await _httpClient.GetFromJsonAsync<APIResponse<T>>(url);
            if (res == null) throw new Exception("response bị null");
            return res;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> AddProduct<T>(T product) where T : class
    {
        try
        {
            var req = await _httpClient.PostAsJsonAsync($"api/v1/product", product);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            if (res == null) throw new Exception("response bị null");

            return res;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> EditProduct<T>(int id, T product) where T : class
    {
        try
        {
            var req = await _httpClient.PutAsJsonAsync($"api/v1/product/{id}", product);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            if (res == null) throw new Exception("response bị null");

            return res;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> UploadImage<T>(IBrowserFile file) where T : class
    {
        try
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10_000_000));
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            content.Add(streamContent, "file", file.Name);
            Console.WriteLine(file.ContentType);

            var req = await _httpClient.PostAsync("api/v1/image", content);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            if (res == null) throw new Exception("response bị null");

            return res;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
