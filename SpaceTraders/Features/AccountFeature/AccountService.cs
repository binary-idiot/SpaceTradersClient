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
		Dictionary<string, string> headers = (string.IsNullOrWhiteSpace(token))
			? new Dictionary<string, string>()
			: new Dictionary<string, string>() { { "Authorization", $"Bearer {token}" } };
		return await _apiService.Get<Account>(new ApiQuery() { Endpoint = "my/account", Headers = headers});
	}
}