using SpaceTraders.Shared.Models.API;
using SpaceTraders.Shared.Services;
using SpaceTraders.Shared.Services.API;

namespace SpaceTraders.Features.AccountFeature;

public class AccountService : IDataService
{
	private readonly GameApiService _apiService;

	public AccountService(GameApiService apiService)
	{
		_apiService = apiService;
	}

	public async Task<ApiResponse<Account>> GetAccount(string? token = "")
	{
		ApiQuery query = new ApiQuery()
		{
			Endpoint = "my/agent",
			Authorization = token
		};
		
		return await _apiService.Get<Account>(query);
	}
}