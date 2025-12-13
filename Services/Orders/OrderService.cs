
using Blazor_FE.Models;
using Blazor_FE.Models.Dtos;
using Blazor_FE.Services.Base;
using Microsoft.AspNetCore.Http;

namespace Blazor_FE.Services.Orders;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<T> PlaceOrderAsync<T>(OrderDTO orderDTO)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/order/checkout/banking", orderDTO);
            var result = await response.Content.ReadFromJsonAsync<T>();
            if (result is APIResponse<CheckoutResponse> apiRes)
            {
                return result;
            }
            else
            {
                throw new Exception("Unexpected response type");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
