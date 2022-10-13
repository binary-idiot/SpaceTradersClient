using System.Net.Http.Headers;
using Fluxor;
using SpaceTraders.Features.LoginFeature.State;

namespace SpaceTraders.Shared.Utilities;

public class GameAuthHandler : DelegatingHandler
{
	private readonly IState<LoginState> _loginState;

	public GameAuthHandler(IState<LoginState> loginState)
	{
		_loginState = loginState;
	}

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (!string.IsNullOrWhiteSpace(_loginState.Value.Login?.Token))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _loginState.Value.Login.Token);
		}
		return base.SendAsync(request, cancellationToken);
	}
}