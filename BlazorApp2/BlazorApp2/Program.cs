using BlazorApp2;
using BlazorApp2.Provider;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomHttpHandler>();
builder.Services.AddHttpClient("APIshecka", options =>
{
    options.BaseAddress = new Uri("http://192.168.0.7:5155/");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//builder.Services.AddOptions();

await builder.Build().RunAsync();
