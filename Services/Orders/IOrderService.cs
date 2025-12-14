using Blazor_FE.Models;
using Blazor_FE.Models.Dtos;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Orders;

public interface IOrderService
{
    public Task<T> PlaceOrderAsync<T>(OrderDTO orderDTO);
    public Task<APIResponse<List<OrderModel>>> GetAllOrdersAsync(int page, int pageSize);


}
