using Blazor_FE.Models;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Categories;

public interface ICategoryService
{
    public Task<APIResponse<List<CategoryModel>>> GetCategoriesAsync();
}
