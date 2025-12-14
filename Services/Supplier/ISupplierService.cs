using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Supplier;

public interface ISupplierService
{
    public Task<APIResponse<T>> GetAllAsync<T>();
}