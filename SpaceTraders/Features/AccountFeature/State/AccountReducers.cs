using Fluxor;

namespace SpaceTraders.Features.AccountFeature.State;

public class AccountReducers
{
	[ReducerMethod(typeof(GetAccountAction))]
	public static AccountState ReduceGetAccountAction(AccountState state) =>
		new AccountState(
			account: state.Account ?? new Account(),
			isLoading: true,
			currentErrorMessage: null
		);

	[ReducerMethod]
	public static AccountState ReduceGetAccountSuccessAction(AccountState state, GetAccountSuccessAction action) =>
		new AccountState(
			account: action.Account,
			isLoading: false,
			currentErrorMessage: null
		);
	
	[ReducerMethod]
	public static AccountState ReduceGetAccountFailureAction(AccountState state, GetAccountFailureAction action) =>
		new AccountState(
			account: state.Account,
			isLoading: false,
			currentErrorMessage: action.ErrorMessage
		);
}