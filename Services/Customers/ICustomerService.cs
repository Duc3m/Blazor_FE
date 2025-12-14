using Blazor_FE.Models;
using Blazor_FE.Models.Dtos;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Customers.dtos;

namespace Blazor_FE.Services.Customers
{
    public interface ICustomerService
    {
        public Task<APIResponse<CustomerModel>> GetCustomerByAccountIdAsync(int accountId);
        public Task<APIResponse<CustomerModel>> UpdateCustomerAsync(int customerId, CustomerRequestDTO requestDto);
    }
}
