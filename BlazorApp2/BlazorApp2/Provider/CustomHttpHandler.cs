using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace BlazorApp2.Provider
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private ILocalStorageService _localStorageService;

        public CustomHttpHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>("jwt-token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
