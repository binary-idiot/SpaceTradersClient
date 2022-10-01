using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpaceTraders;
using SpaceTraders.Shared.Services.API;
using SpaceTraders.Shared.Services.Data;
using SpaceTraders.Shared.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<GameApiService>();
builder.Services.AddHttpClient<GameApiService>(client =>
	client.BaseAddress = new Uri(builder.Configuration["GameApiBase"]));

builder.Services.RegisterTransientServices<IDataService>();

await builder.Build().RunAsync();