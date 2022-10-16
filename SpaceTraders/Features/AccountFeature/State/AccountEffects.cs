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
		using (IServiceScope scope = _serviceScopeFactory.CreateScope())
		{
			AccountService accountService = scope.ServiceProvider.GetService<AccountService>();
			if (accountService != null)
			{
				ApiResponse<Account> accountResponse = await accountService.GetAccount();

				if (accountResponse.Success)
				{
					dispatcher.Dispatch(new GetAccountSuccessAction(accountResponse.Result));
				}
				else
				{
					dispatcher.Dispatch(new GetAccountFailureAction(((Error)accountResponse.Error).Message));
				}
			}
			else
			{
				dispatcher.Dispatch(new GetAccountFailureAction("Could not load required services"));
			}
		}
	}
}