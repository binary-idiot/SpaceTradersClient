using Fluxor;
using SpaceTraders.Shared.State;

namespace SpaceTraders.Features.AccountFeature.State;

[FeatureState]
public class AccountState : RootState
{
	public Account? Account { get; }

	public AccountState() {}

	public AccountState(Account? account, bool isLoading = false, string? currentErrorMessage = null) 
		: base(isLoading, currentErrorMessage)
	{
		Account = account;
	}
}