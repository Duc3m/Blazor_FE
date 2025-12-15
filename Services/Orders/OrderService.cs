
using Blazor_FE.Models;
using Blazor_FE.Models.Dtos;
using Blazor_FE.Models.ViewModels;
using Blazor_FE.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using VNPAY.Models;

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

    public async Task<APIResponse<OrderViewModel>> GetOrderByIdAsync(int orderId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<APIResponse<OrderViewModel>>($"api/v1/order/{orderId}");
            return response!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<APIResponse<List<OrderModel>>> GetOrdersByCustomerIdAsync(int customerId)
    {
        try
        {
            var url = $"api/v1/order/customer/{customerId}";
            var response = await _httpClient.GetFromJsonAsync<APIResponse<List<OrderModel>>>(url);
            return response!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<PaymentAPIResponse> CreatePaymentUrlAsync(VnpayPaymentRequest paymentRequest)
    {
        try
        {
            var response = _httpClient.PostAsJsonAsync("api/v1/payment/vnpay-payment", paymentRequest);
            var result = response.Result.Content.ReadFromJsonAsync<PaymentAPIResponse>();
            return result;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<T> RepayOrderAsync<T>(int orderId)
    {
        try
        {
            string url = $"api/v1/order/checkout/repayOrder/{orderId}";
            var response = await _httpClient.GetAsync(url);
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
