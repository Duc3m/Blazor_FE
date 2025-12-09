using Blazor_FE.Models;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<APIResponse<List<CategoryDTO>>> GetCategoriesAsync()
    {
        try {
            var url = "api/v1/category";

            var response =  _httpClient.GetFromJsonAsync<APIResponse<List<CategoryDTO>>>(url);
            return await response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
