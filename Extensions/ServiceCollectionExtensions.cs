using Blazor_FE.Services.Products;
using Blazor_FE.Services.Auth;

namespace Blazor_FE.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, string baseAddress)
    {
        Action<HttpClient> configure = client =>
            client.BaseAddress = new Uri(baseAddress);

        services.AddHttpClient<IProductService, ProductService>(configure);
        services.AddHttpClient<IAuthService, AuthService>(configure);
        return services;
    }
}
