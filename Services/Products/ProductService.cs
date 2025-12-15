using System.Globalization;
using Blazor_FE.Models;
using Blazor_FE.Models.Product;
using Blazor_FE.Services.Products.dtos;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Products.dtos;
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
            var queryParams = new Dictionary<string, string>
            {
                ["pageNumber"] = page.ToString(),
                ["pageSize"] = pageSize.ToString()
            };
            
            if (!string.IsNullOrEmpty(filter.ProductName)) queryParams["productName"] = filter.ProductName;
            if (filter.MaxPrice.HasValue && filter.MinPrice.HasValue && 
                filter.MaxPrice.Value > 0 && filter.MinPrice.Value > 0)
            {
                queryParams["maxPrice"] = filter.MaxPrice.Value.ToString(CultureInfo.InvariantCulture);
                queryParams["minPrice"] = filter.MinPrice.Value.ToString(CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(filter.Category)) queryParams["category"] = filter.Category;

            var url = QueryHelpers.AddQueryString("api/v1/product/search", queryParams);
            var res = await _httpClient.GetFromJsonAsync<APIResponse<T>>(url);
            return res ?? throw new Exception("response bị null");;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> AddAsync<T>(T product) where T : class
    {
        try
        {            
            var req = await _httpClient.PostAsJsonAsync($"api/v1/product", product);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            return res ?? throw new Exception("response bị null");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> EditAsync<T>(int id, T product) where T : class
    {
        try
        {
            var req = await _httpClient.PutAsJsonAsync($"api/v1/product/{id}", product);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            return res ?? throw new Exception("response bị null");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> DeleteAsync<T>(int id) where T : class
    {
        try
        {
            var res = await _httpClient.DeleteAsync($"api/v1/product/{id}");
            var result = await res.Content.ReadFromJsonAsync<APIResponse<T>>();
            return result ?? throw new Exception("Response bi null");
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
            return res ?? throw new Exception("response bị null");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
