using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Products.dtos;
using Microsoft.AspNetCore.Components.Forms;
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
    
    
    public async Task<APIResponse<List<T>>?> SearchProductPageAsync<T>(int page, int pageSize)
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = page.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            var url = QueryHelpers.AddQueryString("api/v1/product/search", queryParams);

            var response = _httpClient.GetFromJsonAsync<APIResponse<List<T>>>(url);
            return await response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }   
    }
    
    public async Task<APIResponse<T>> AddProduct<T>(T product) 
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
    
    public async Task<APIResponse<T>> EditProduct<T>(int id, T product)
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
    
    public async Task<APIResponse<string>> UploadImage(IBrowserFile file)
    {
        try
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10_000_000));
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            
            content.Add(streamContent, "file", file.Name);
            Console.WriteLine(file.ContentType);
            
            var req = await _httpClient.PostAsync("api/v1/image", content);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<string>>();
            if (res == null) throw new Exception("response bị null"); 
            
            return res;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
