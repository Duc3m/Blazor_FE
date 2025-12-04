using Blazor_FE.Models;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Products;

public interface IProductService
{
    public Task<APIResponse<List<ProductDTO>>> GetProductsAsync();
    public Task<APIResponse<List<ProductDTO>>> GetProductsPageAsync(int page, int pageSize);
}
