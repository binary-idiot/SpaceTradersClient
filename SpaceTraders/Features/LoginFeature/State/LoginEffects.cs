using Fluxor;
using SpaceTraders.Features.AccountFeature;
using SpaceTraders.Shared.Models;
using SpaceTraders.Shared.Models.API;

namespace SpaceTraders.Features.LoginFeature.State;

public class LoginEffects
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public LoginEffects(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	[EffectMethod(typeof(LoadLoginAction))]
	public async Task HandleLoadLoginAction(IDispatcher dispatcher)
	{
		using (IServiceScope scope = _serviceScopeFactory.CreateScope())
		{
			LoginService? loginService = scope.ServiceProvider.GetService<LoginService>();
			if (loginService is not null)
			{
				Login login = await loginService.GetSavedLogin();
				dispatcher.Dispatch(new LoadLoginSuccessAction(login));
			}
			else
			{
				dispatcher.Dispatch(new LoadLoginFailureAction("LoginService is null"));
			}
		}
	}
	
	[EffectMethod]
	public async Task HandleLoginAction(LoginAction action, IDispatcher dispatcher)
	{
		using (IServiceScope scope = _serviceScopeFactory.CreateScope())
		{
			LoginService loginService = scope.ServiceProvider.GetService<LoginService>();
			AccountService accountService = scope.ServiceProvider.GetService<AccountService>();
			if (loginService != null && accountService != null)
			{
				ApiResponse<Account> accountResponse = await accountService.GetAccount(action.Login.Token);

				if (accountResponse.Success)
				{
					action.Login.Username = accountResponse.Result.Username;
					await loginService.SetSavedLogin(action.Login);
					dispatcher.Dispatch(new LoginSuccessAction(action.Login));
				}
				else
				{
					dispatcher.Dispatch(new LoginFailureAction(((Error)accountResponse.Error).Message));
				}
			}
			else
			{
				dispatcher.Dispatch(new LoginFailureAction("Could not load required services"));
			}
		}
	}
}