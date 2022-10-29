using Fluxor;
using SpaceTraders.Shared.Models;
using SpaceTraders.Shared.Models.API;

namespace SpaceTraders.Features.AccountFeature.State;

public class AccountEffects
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public AccountEffects(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	[EffectMethod(typeof(GetAccountAction))]
	public async Task HandleGetAccountAction(IDispatcher dispatcher)
	{
		using IServiceScope scope = _serviceScopeFactory.CreateScope();
		try
		{
			AccountService accountService = scope.ServiceProvider.GetRequiredService<AccountService>();
			ApiResponse<Account> accountResponse = await accountService.GetAccount();

			if (accountResponse.Success)
			{
				dispatcher.Dispatch(new GetAccountSuccessAction(accountResponse.Result));
			}
			else
			{
				throw new Exception(((Error)accountResponse.Error).Message);
			}
		}
		catch(Exception ex)
		{
			ILogger<AccountEffects> logger = scope.ServiceProvider.GetRequiredService<ILogger<AccountEffects>>();
			logger.LogError(ex.ToString());
			dispatcher.Dispatch(new GetAccountFailureAction(ex.Message));
		}
	}
}