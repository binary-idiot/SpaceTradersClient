using Fluxor;
using SpaceTraders.Models.User;

namespace SpaceTraders.State.LoginFeature;

[FeatureState]
public class LoginState : RootState
{
	public Login? Login { get; }
	public bool IsLoggedIn { get; }
	
	public LoginState() {}
	public LoginState(Login? login, bool isLoggedIn, bool isLoading = false, string? currentErrorMessage = null) 
		: base(isLoading, currentErrorMessage)
	{
		Login = login;
		IsLoggedIn = isLoggedIn;
	}
}