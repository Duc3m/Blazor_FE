using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Categories.dtos;
using Microsoft.AspNetCore.WebUtilities;

namespace Blazor_FE.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<APIResponse<List<CategoryModel>>> GetCategoriesAsync()
    {
        try {
            var url = "api/v1/category";

            var response =  _httpClient.GetFromJsonAsync<APIResponse<List<CategoryModel>>>(url);
            return await response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task<APIResponse<List<T>>> SearchAsync<T>(SearchCategoryDTO dto)
    {
        try {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = dto.PageNumber.ToString(),
                ["pageSize"] = dto.PageSize.ToString()
            };
            
            if (!string.IsNullOrEmpty(dto.CategoryName)) queryParams["categoryName"] = dto.CategoryName;

            var url = QueryHelpers.AddQueryString("api/v1/category/search", queryParams);
            var res = await _httpClient.GetFromJsonAsync<APIResponse<List<T>>>(url);
            return res ?? throw new Exception("response bị null");;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> AddAsync<T>(T category)
    {
        try
        {            
            var req = await _httpClient.PostAsJsonAsync($"api/v1/category", category);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            return res ?? throw new Exception("response bị null");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> EditAsync<T>(int id,T category)
    {
        try
        {            
            var req = await _httpClient.PutAsJsonAsync($"api/v1/category/{id}", category);
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            return res ?? throw new Exception("response bị null");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<T>> DeleteAsync<T>(int id)
    {
        try
        {            
            var req = await _httpClient.DeleteAsync($"api/v1/category/{id}");
            var res = await req.Content.ReadFromJsonAsync<APIResponse<T>>();
            return res ?? throw new Exception("response bị null");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
