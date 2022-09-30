using SpaceTraders.Shared.Models;
using SpaceTraders.Shared.Models.Game;

namespace SpaceTraders.Shared.Services.API;

public class GameApiService : ApiService<Error>
{
	public GameApiService(HttpClient client) : base(client)
	{ }
}