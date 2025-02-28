using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
namespace BlazorApp2.Provider
{

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Получаем токен из localStorage
            var token = await _localStorage.GetItemAsync<string>("jwt-token");

            if (string.IsNullOrEmpty(token))
            {
                // Если токена нет, пользователь не аутентифицирован
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Если токен есть, создаем ClaimsPrincipal
            var claims = new[] { new Claim(ClaimTypes.Name, "User") }; // Добавьте реальные claims
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "User") }; // Добавьте реальные claims
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}
