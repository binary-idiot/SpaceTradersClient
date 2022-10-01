using SpaceTraders.Shared.Models.Game;
using SpaceTraders.Shared.Services.API;

namespace SpaceTraders.Shared.Services.Data;

public class GameStatusService : IDataService
{
	private readonly GameApiService _api;

	public GameStatusService(GameApiService api)
	{
		_api = api;
	}

	public async Task<GameStatus> GetStatus()
	{
		ApiResponse<GameStatus> response = await _api.Get<GameStatus>(new ApiQuery()
		{
			Endpoint = "game/status"
		});

		if (response.Success)
		{
			return response.Result;
		}
		
		return new GameStatus()
		{
			Status = "Error connecting to game"
		};
	}
}