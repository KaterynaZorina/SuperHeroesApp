using Microsoft.AspNetCore.Authorization;
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
            services.AddAuthorizationCore();
            services.AddSingleton<ISuperHeroesService, SuperHeroesService>();
            services.AddSingleton<IAuthService, LocalStorageAuthorizationService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<AuthenticationStateProvider, LocalStorageAuthenticationStateProvider>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
