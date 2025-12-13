using Blazor_FE.Models;

namespace Blazor_FE.Services.Base;

public class CheckoutResponse
{
    public OrderModel? Order { get; set; }
    public string? PaymentUrl { get; set; }
}
