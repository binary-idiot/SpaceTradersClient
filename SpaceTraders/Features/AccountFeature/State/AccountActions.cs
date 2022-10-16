using SpaceTraders.Shared.State;

namespace SpaceTraders.Features.AccountFeature.State;

public abstract class BaseAccountAction : IAction
{
	public Account Account { get; }

	public BaseAccountAction(Account account)
	{
		Account = account;
	}
}

public class GetAccountAction : IAction {}

public class GetAccountSuccessAction : BaseAccountAction
{
	public GetAccountSuccessAction(Account account) : base(account) { }
}

public class GetAccountFailureAction : FailureAction
{
	public GetAccountFailureAction(string errorMessage) : base(errorMessage) { }
}