
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
            if (request.RequestUri.AbsolutePath.ToLower().Contains("User/login"))
            {
                return await base.SendAsync(request, cancellationToken);

            }

            var token = await _localStorageService.GetItemAsync<string>("jwt-token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("User/login", $"Bearer {token}");
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
