using Blazor_FE.Models;
using Blazor_FE.Models.Product;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Products.dtos;
using Microsoft.AspNetCore.Components.Forms;
//using ProductDTO = Blazor_FE.Models.ProductDTO;

namespace Blazor_FE.Services.Products;

public interface IProductService
{
    public Task<APIResponse<List<ProductModel>>> GetProductsAsync();
    public Task<APIResponse<List<ProductModel>>> GetProductsPageAsync(int page, int pageSize);
    public Task<APIResponse<List<ProductModel>>> FilterProductAsync(ProductFilterRequest filterRequest, int page, int pageSize);
    public Task<APIResponse<T>> SearchAsync<T>(ProductFilterDTO filter, int page, int pageSize) where T : class;
    public Task<APIResponse<T>> AddAsync<T>(T product) where T : class;
    public Task<APIResponse<T>> EditAsync<T>(int id, T product) where T : class;
    public Task<APIResponse<T>> DeleteAsync<T>(int id) where T : class;
    public Task<APIResponse<T>> UploadImage<T>(IBrowserFile file) where T : class;
}
