using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpaceTraders;
using SpaceTraders.Services.API;
using SpaceTraders.Services.Data;
using SpaceTraders.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddTransient<GameApiService>();
builder.Services.AddHttpClient<GameApiService>(client =>
	client.BaseAddress = new Uri(builder.Configuration["GameApiBase"]));

builder.Services.AddTransientServicesWithInterface<IDataService>();

await builder.Build().RunAsync();