using SpaceTraders.Shared.Models;

namespace SpaceTraders.Shared.Services.API;

public class GameApiService : ApiService<Error>
{
	public GameApiService(HttpClient client, ILogger<GameApiService> logger, IServiceProvider serviceProvider) 
		: base(client, logger, serviceProvider)
	{ }
}