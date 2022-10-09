namespace SpaceTraders.Shared.State;

public abstract class RootState
{
	public bool IsLoading { get; }
	public string? CurrentErrorMessage { get; }
	public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);

	public RootState() 
		: this(false, null) { }
	public RootState(bool isLoading, string? currentErrorMessage)
	{
		IsLoading = isLoading;
		CurrentErrorMessage = currentErrorMessage;
	}
}