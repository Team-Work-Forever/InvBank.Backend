using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using InvBank.Web;
using InvBank.Web.Helper;
using InvBank.Web.Provider;
using InvBank.Service;
using InvBank.Web.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHelper();
builder.Services.AddProviders();
builder.Services.AddServices();
builder.Services.AddAuthorization();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5099") });

await builder.Build().RunAsync();
