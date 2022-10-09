using Fluxor;

namespace SpaceTraders.Features.LoginFeature.State;

public static class LoginReducers
{
	[ReducerMethod(typeof(LoadLoginAction))]
	public static LoginState ReduceLoadLoginAction(LoginState state) =>
		new LoginState(
			login: null,
			isLoggedIn: false,
			isLoading: true
		);
	
	[ReducerMethod]
	public static LoginState ReduceLoadLoginSuccessAction(LoginState state, LoadLoginSuccessAction action) =>
		new LoginState(
			login: action.Login,
			isLoggedIn: false,
			isLoading: false
		);
	
	[ReducerMethod]
	public static LoginState ReduceLoadLoginFailureAction(LoginState state, LoadLoginFailureAction action) =>
		new LoginState(
			login: null,
			isLoggedIn: false,
			isLoading: false,
			currentErrorMessage: action.ErrorMessage
		);
	
	[ReducerMethod]
	public static LoginState ReduceLoginAction(LoginState state, LoginAction action) =>
		new LoginState(
			login: new Login() { Token = action.Login.Token },
			isLoggedIn: false,
			isLoading: true
		);
	
	[ReducerMethod]
	public static LoginState ReduceLoginSuccessAction(LoginState state, LoginSuccessAction action) =>
		new LoginState(
			login: action.Login,
			isLoggedIn: true,
			isLoading: false
		);
	
	[ReducerMethod]
	public static LoginState ReduceLoginFailureAction(LoginState state, LoginFailureAction action) =>
		new LoginState(
			login: null,
			isLoggedIn: true,
			isLoading: false,
			currentErrorMessage: action.ErrorMessage
		);

	[ReducerMethod]
	public static LoginState ReduceRegisterAction(LoginState state, RegisterAction action) =>
		new LoginState(
			login: new Login() { Username = action.Login.Username },
			isLoggedIn: false,
			isLoading: true
		);
	
	[ReducerMethod]
	public static LoginState ReduceRegisterSuccessAction(LoginState state, RegisterSuccessAction action) =>
		new LoginState(
			login: action.Login,
			isLoggedIn: true,
			isLoading: false
		);
	
	[ReducerMethod]
	public static LoginState ReduceRegisterFailureAction(LoginState state, RegisterFailureAction action) =>
		new LoginState(
			login: null,
			isLoggedIn: true,
			isLoading: false,
			currentErrorMessage: action.ErrorMessage
		);

	[ReducerMethod(typeof(LogoutAction))]
	public static LoginState ReduceLogoutAction(LoginState state) =>
		new LoginState(
			login: null,
			isLoggedIn: state.IsLoggedIn,
			isLoading: true
		);
	
	[ReducerMethod(typeof(LogoutSuccessAction))]
	public static LoginState ReduceLogoutSuccessAction(LoginState state) =>
		new LoginState(
			login: null,
			isLoggedIn: false,
			isLoading: false
		);
	
	[ReducerMethod]
	public static LoginState ReduceLogoutFailureAction(LoginState state, LogoutFailureAction action) =>
		new LoginState(
			login: null,
			isLoggedIn: state.IsLoggedIn,
			isLoading: false,
			currentErrorMessage: action.ErrorMessage
		);
}