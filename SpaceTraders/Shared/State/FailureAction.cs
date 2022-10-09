namespace SpaceTraders.Shared.State;

public abstract class FailureAction : IAction
{
	public string ErrorMessage { get; }

	public FailureAction(string errorMessage)
	{
		ErrorMessage = errorMessage;
	}
}