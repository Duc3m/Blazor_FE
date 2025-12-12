using Blazor_FE.Models;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Products;

public interface IProductService
{
    public Task<APIResponse<List<ProductModel>>> GetProductsAsync();
    public Task<APIResponse<List<ProductModel>>> GetProductsPageAsync(int page, int pageSize);
}
