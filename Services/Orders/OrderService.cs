
using Blazor_FE.Models;
using Blazor_FE.Models.Dtos;
using Blazor_FE.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

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

    public async Task<APIResponse<List<OrderModel>>> GetAllOrdersAsync(int page, int pageSize)
    {
        try {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = page.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            var url = QueryHelpers.AddQueryString("api/v1/order", queryParams);

            var response = await _httpClient.GetFromJsonAsync<APIResponse<List<OrderModel>>>(url);
            return response!;
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
}
