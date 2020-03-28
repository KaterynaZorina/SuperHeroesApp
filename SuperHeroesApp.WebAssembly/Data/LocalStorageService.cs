using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using SuperHeroesApp.WebAssembly.Data.Models;

namespace SuperHeroesApp.WebAssembly.Data
{
    public interface ILocalStorageService
    {
        Task<bool> StoreUserInfo(UserData userData);

        ValueTask ClearUserInfo();

        Task<UserInfo> GetUserInfo();
    }
    
    public class LocalStorageService: ILocalStorageService
    {
        private const string AuthKey = "UserData";
        private readonly IJSRuntime _jsRuntime;
        
        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        
        public async Task<bool> StoreUserInfo(UserData userData)
        {
            var userDataStr = JsonSerializer.Serialize(userData);

            return await _jsRuntime.InvokeAsync<bool>("storeUserDataToLocalStorage", AuthKey, userDataStr);
        }

        public ValueTask ClearUserInfo()
        {
            return _jsRuntime.InvokeVoidAsync("clearUserDataInLocalStorage", AuthKey);
        }

        public async Task<UserInfo> GetUserInfo()
        {
            var userInfoStr = await _jsRuntime.InvokeAsync<string>("getUserData", AuthKey);

            if (string.IsNullOrWhiteSpace(userInfoStr))
                return null;

            var userData = JsonSerializer.Deserialize<UserData>(userInfoStr);

            return new UserInfo {Name = userData.UserName};
        }
    }
}