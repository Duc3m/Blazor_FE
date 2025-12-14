using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Customers.dtos;

namespace Blazor_FE.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<CustomerModel>> GetCustomerByAccountIdAsync(int accountId)
        {
            try
            {
                var url = $"api/v1/Customer/user/{accountId}";

                var response = await _httpClient.GetFromJsonAsync<APIResponse<CustomerModel>>(url);
                Console.WriteLine(response);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<APIResponse<CustomerModel>> UpdateCustomerAsync(int customerId, CustomerRequestDTO requestDto)
        {
            try
            {
                var url = $"api/v1/Customer/{customerId}";
                var response = await _httpClient.PutAsJsonAsync(url, requestDto);
                if (!response.IsSuccessStatusCode)
                {
                    return new APIResponse<CustomerModel>
                    {
                        StatusCode = (int)response.StatusCode,
                        Message = "Failed to update customer",
                        Content = null
                    };
                }

                var result = await response.Content.ReadFromJsonAsync<APIResponse<CustomerModel>>();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
