using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using SuperHeroesApp.WebAssembly.Data.Models;
using SuperHeroesApp.WebAssembly.Data.Models.Login;
using SuperHeroesApp.WebAssembly.Data.Models.Register;

namespace SuperHeroesApp.WebAssembly.Data
{
    public interface IAuthService
    {
        Task<RegisterOutput> RegisterAsync(RegisterInput userData);

        Task<LoginOutput> LoginAsync(LoginInput loginData);

        ValueTask LogoutAsync();
    }
    
    public class LocalStorageAuthorizationService : IAuthService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        
        // TODO: Add fake provider
        private readonly List<UserAuthData> _userDatas = new List<UserAuthData>();

        public LocalStorageAuthorizationService(ILocalStorageService localStorageService, 
            AuthenticationStateProvider authenticationStateProvider)
        {
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }
        
        public Task<RegisterOutput> RegisterAsync(RegisterInput userData)
        {
            Console.WriteLine($"Register user data is not null: {userData != null}");
            
            if(userData == null)
                throw new ArgumentNullException(nameof(userData));
            
            var existingItem =
                _userDatas.FirstOrDefault(i => i.Email.Equals(userData.Email, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"User with email: {userData.Email} exists: {existingItem != null}");
            
            if (existingItem != null)
            {
                return Task.FromResult(new RegisterOutput().Error($"User with {userData.Email} already exists"));
            }
            
            var data = new UserAuthData
            {
                Email = userData.Email,
                UserName = userData.Name,
                Password = userData.Password,
            };
            
            _userDatas.Add(data);

            return Task.FromResult(new RegisterOutput().Success());
        }

        public async Task<LoginOutput> LoginAsync(LoginInput loginData)
        {
            Console.WriteLine($"Login data is not null: {loginData != null}");
            
            if(loginData == null)
                throw new ArgumentNullException(nameof(loginData));

            Console.WriteLine($"User datas is not null or empty: {_userDatas?.Any()}");
            Console.WriteLine($"User datas is: {JsonSerializer.Serialize(_userDatas)}");
            
            var existingItem =
                _userDatas.FirstOrDefault(i => 
                    i.Email.Equals(loginData.Email, StringComparison.OrdinalIgnoreCase) 
                    && i.Password.Equals(loginData.Password, StringComparison.OrdinalIgnoreCase));
            
            if (existingItem == null)
            {
                return new LoginOutput().Error("User with such login/password doesn't exist");
            }

            existingItem.Token = Guid.NewGuid().ToString();
            
            var userInfo = new UserData
            {
                Email = existingItem.Email,
                Token = existingItem.Token,
                UserName = existingItem.UserName
            };
            
            var result = await _localStorageService.StoreUserInfo(userInfo);

            if (result)
            {
                ((LocalStorageAuthenticationStateProvider) _authenticationStateProvider)
                    .NotifyAuthenticationStateChanged(userInfo.Email);
            }
            
            return result
                ? new LoginOutput().Success()
                : new LoginOutput().Error("Something went wrong. Please try later :)");
        }

        public async ValueTask LogoutAsync()
        {
            await _localStorageService.ClearUserInfo();
            
            ((LocalStorageAuthenticationStateProvider) _authenticationStateProvider)
                .NotifyAuthenticationStateChanged();
        }
    }
}