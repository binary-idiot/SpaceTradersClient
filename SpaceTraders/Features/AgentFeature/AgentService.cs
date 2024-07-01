using SpaceTraders.Shared.Models.API;
using SpaceTraders.Shared.Services;
using SpaceTraders.Shared.Services.API;

namespace SpaceTraders.Features.AgentFeature;

public class AgentService : IDataService
{
	private readonly GameApiService _apiService;

	public AgentService(GameApiService apiService)
	{
		_apiService = apiService;
	}

	public async Task<ApiResponse<Agent>> GetAgent(string? token = "")
	{
		ApiQuery query = new ApiQuery()
		{
			Endpoint = "my/agent",
			Authorization = token
		};
		
		return await _apiService.Get<Agent>(query);
	}
}