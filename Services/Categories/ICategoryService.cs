using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Categories.dtos;

namespace Blazor_FE.Services.Categories;

public interface ICategoryService
{
    public Task<APIResponse<List<CategoryModel>>> GetCategoriesAsync();
    public Task<APIResponse<List<T>>> SearchAsync<T>(SearchCategoryDTO dto);
    public Task<APIResponse<T>> AddAsync<T>(T category);
    public Task<APIResponse<T>> EditAsync<T>(int id, T category);
    public Task<APIResponse<T>> DeleteAsync<T>(int id);
}
