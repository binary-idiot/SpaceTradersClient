using Fluxor;

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
			if (loginService is not null)
			{
				await loginService.SetSavedLogin(action.Login);
				dispatcher.Dispatch(new LoginSuccessAction(action.Login));
			}
			else
			{
				dispatcher.Dispatch(new LoginFailureAction("LoginService is null"));
			}
		}
	}
}