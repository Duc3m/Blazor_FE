using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Products.dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazor_FE.Services.Products;

public interface IProductService
{
    public Task<APIResponse<List<ProductModel>>> GetProductsAsync();
    public Task<APIResponse<List<ProductModel>>> GetProductsPageAsync(int page, int pageSize);
    
    // -- -- //
    public Task<APIResponse<List<T>>?> SearchProductPageAsync<T>(int page, int pageSize);
    public Task<APIResponse<T>> AddProduct<T>(T product);
    public Task<APIResponse<T>> EditProduct<T>(int id, T product);
    public Task<APIResponse<string>> UploadImage(IBrowserFile file);
}
