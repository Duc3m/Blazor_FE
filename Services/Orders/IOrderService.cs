using Blazor_FE.Models.Dtos;

namespace Blazor_FE.Services.Orders;

public interface IOrderService
{
    public Task<T> PlaceOrderAsync<T>(OrderDTO orderDTO);
}
