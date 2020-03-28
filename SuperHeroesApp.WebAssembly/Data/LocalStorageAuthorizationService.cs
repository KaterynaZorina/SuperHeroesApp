using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        private readonly List<UserAuthData> _userDatas;

        public LocalStorageAuthorizationService()
        {
            _userDatas = new List<UserAuthData>();
        }

        public LocalStorageAuthorizationService(ILocalStorageService localStorageService): this()
        {
            _localStorageService = localStorageService;
        }
        
        public Task<RegisterOutput> RegisterAsync(RegisterInput userData)
        {
            var existingItem =
                _userDatas.FirstOrDefault(i => i.Email.Equals(userData.Email, StringComparison.OrdinalIgnoreCase));

            if (existingItem != null)
            {
                return Task.FromResult(new RegisterOutput().Error($"User with {userData.Email} already exists"));
            }
            
            var data = new UserAuthData
            {
                UserName = userData.Email,
                Password = userData.Password,
            };
            
            _userDatas.Add(data);

            return Task.FromResult(new RegisterOutput().Success());
        }

        public async Task<LoginOutput> LoginAsync(LoginInput loginData)
        {
            var existingItem =
                _userDatas.FirstOrDefault(i => 
                    i.Email.Equals(loginData.Email, StringComparison.OrdinalIgnoreCase) 
                    && i.Password.Equals(loginData.Password, StringComparison.OrdinalIgnoreCase));
            
            if (existingItem != null)
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

            return result
                ? new LoginOutput().Success()
                : new LoginOutput().Error("Something went wrong. Please try later :)");
        }

        public ValueTask LogoutAsync()
        {
            return _localStorageService.ClearUserInfo();
        }
    }
}