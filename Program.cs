using Blazor_FE.Components;
using Blazor_FE.Extensions;
using Blazor_FE.Services.Auth;
using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddBlazorise()
    .AddTailwindProviders()
    .AddFontAwesomeIcons();

builder.Services.AddServices("https://localhost:8081/");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddAuthentication(options =>
{
    // Thiết lập Scheme mặc định là JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        // Cấu hình validate token (để server hiểu token hợp lệ)
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Có thể để true nếu server API có set Issuer
            ValidateAudience = false, // Có thể để true nếu server API có set Audience
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            // QUAN TRỌNG: Key này phải TRÙNG KHỚP với Key mà API dùng để tạo Token
            // Nếu bạn không có Key ở đây, bạn có thể comment dòng này lại để chạy tạm (nhưng kém bảo mật)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Key-bi-mat-dai-tren-32-ky-tu-giong-ben-api")),
            RoleClaimType = "role"
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
