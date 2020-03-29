using System;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace SuperHeroesApp.WebAssembly.Data
{
    public class LocalStorageAuthenticationStateProvider: AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public LocalStorageAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userData = await _localStorageService.GetUserInfo();

            Console.WriteLine($"User data: {JsonSerializer.Serialize(userData)}");
            
            if (userData == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var authResult = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(GetAuthorizedUserClaim(userData.Email))));
            
            Console.WriteLine(
                $"AuthenticationState is {JsonSerializer.Serialize(authResult, new JsonSerializerOptions{ ReferenceHandling = ReferenceHandling.Preserve})}");

            return authResult;
        }

        public void NotifyAuthenticationStateChanged(string email = null)
        {
            ClaimsPrincipal authenticatedUser;

            if (!string.IsNullOrWhiteSpace(email))
            {
                authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(GetAuthorizedUserClaim(email)));
            }
            else
            {
                authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
            }

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            
            NotifyAuthenticationStateChanged(authState);
        }
        
        private static Claim[] GetAuthorizedUserClaim(string email)
        {
            return new []
            {
                new Claim(ClaimTypes.Name, email),
                new Claim("TestUser", "true") 
            };
        }
    }
}