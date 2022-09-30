using SpaceTraders.Shared.Models;

namespace SpaceTraders.Shared.Services.API;

public class GameApiService : ApiService<GameError>
{
	public GameApiService(HttpClient client) : base(client)
	{ }
}