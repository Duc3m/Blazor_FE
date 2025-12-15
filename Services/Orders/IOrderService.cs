using Blazor_FE.Models;
using Blazor_FE.Models.Dtos;
using Blazor_FE.Models.ViewModels;
using Blazor_FE.Services.Base;
using Microsoft.AspNetCore.Mvc;
using VNPAY.Models;

namespace Blazor_FE.Services.Orders;

public interface IOrderService
{
    public Task<T> PlaceOrderAsync<T>(OrderDTO orderDTO);
    public Task<APIResponse<List<OrderModel>>> GetAllOrdersAsync(int page, int pageSize);
    public Task<APIResponse<OrderViewModel>> GetOrderByIdAsync(int orderId);
    public Task<APIResponse<List<OrderModel>>> GetOrdersByCustomerIdAsync(int customerId);
    public Task<PaymentAPIResponse> CreatePaymentUrlAsync(VnpayPaymentRequest paymentRequest);
    public Task<T> RepayOrderAsync<T> (int orderId);
}
