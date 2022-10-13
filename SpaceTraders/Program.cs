using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpaceTraders;
using SpaceTraders.Shared.Services;
using SpaceTraders.Shared.Services.API;
using SpaceTraders.Shared.Utilities;

#if DEBUG
await Task.Delay(1000);
#endif

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddFluxor(options => 
	options.ScanAssemblies(typeof(Program).Assembly)
		.UseReduxDevTools()
);

builder.Services.AddTransient<GameApiService>();
builder.Services.AddHttpClient<GameApiService>(client =>
	client.BaseAddress = new Uri(builder.Configuration["GameApiBase"])
);

builder.Services.AddTransientServicesWithInterface<IDataService>();

await builder.Build().RunAsync();