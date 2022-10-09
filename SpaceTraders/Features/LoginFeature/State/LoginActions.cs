using SpaceTraders.Shared.State;

namespace SpaceTraders.Features.LoginFeature.State;

public abstract class BaseLoginAction : IAction
{
	public Login Login { get; }

	public BaseLoginAction(Login login)
	{
		Login = login;
	}
}

public class LoadLoginAction : IAction {}

public class LoadLoginSuccessAction : BaseLoginAction
{
	public LoadLoginSuccessAction(Login login) : base(login) { }
}

public class LoadLoginFailureAction : FailureAction
{
	public LoadLoginFailureAction(string errorMessage) : base(errorMessage) { }
}

public class LoginAction : BaseLoginAction
{
	public LoginAction(Login login) : base(login) { }
}

public class LoginSuccessAction : BaseLoginAction
{
	public LoginSuccessAction(Login login) : base(login) { }
}

public class LoginFailureAction : FailureAction
{
	public LoginFailureAction(string errorMessage) 
		: base(errorMessage) {} 
}

public class RegisterAction : BaseLoginAction
{
	public RegisterAction(Login login) : base(login) { }
}

public class RegisterSuccessAction : BaseLoginAction
{
	public RegisterSuccessAction(Login login) : base(login) { }
}

public class RegisterFailureAction : FailureAction
{
	public RegisterFailureAction(string errorMessage) 
		: base(errorMessage) {} 
}

public class LogoutAction : IAction {}

public class LogoutSuccessAction : IAction {}

public class LogoutFailureAction : FailureAction
{
	public LogoutFailureAction(string errorMessage) : base(errorMessage) { }
}
