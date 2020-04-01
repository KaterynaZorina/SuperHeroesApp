using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroesApp.ComponentClassLib.Services;
using SuperHeroesApp.WebAssembly.Data;

namespace SuperHeroesApp.WebAssembly
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("DemoPolicy", policy => policy.RequireClaim("TestUser", "true"));
            });
            services.AddSingleton<ISuperHeroesService, SuperHeroesService>();
            services.AddSingleton<IAuthService, LocalStorageAuthorizationService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddSingleton<LocalStorageAuthenticationStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(provider => provider.GetRequiredService<LocalStorageAuthenticationStateProvider>());
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
