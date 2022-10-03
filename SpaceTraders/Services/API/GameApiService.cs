using SpaceTraders.Models.Game;

namespace SpaceTraders.Services.API;

public class GameApiService : ApiService<Error>
{
	public GameApiService(HttpClient client, ILogger<GameApiService> logger) : base(client, logger)
	{ }
}