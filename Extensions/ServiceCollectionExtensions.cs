using Blazor_FE.Services.Auth;
using Blazor_FE.Services.Products;
using Blazor_FE.Services.Categories;

namespace Blazor_FE.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, string baseAddress)
    {
        Action<HttpClient> configure = client =>
            client.BaseAddress = new Uri(baseAddress);

        services.AddHttpClient<IAuthService, AuthService>(configure);
        services.AddHttpClient<IProductService, ProductService>(configure);
        services.AddHttpClient<ICategoryService, CategoryService>(configure);
        return services;
    }
}
