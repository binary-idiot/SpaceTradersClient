namespace SpaceTraders.Shared.Models.Game;

public record ServerData<TModel>
{
	public TModel? Data { get; init; }
	public ServerMetaData? Meta { get; init; }
}

public struct ServerMetaData
{
	public int Total { get; init; }
	public int Page { get; init; }
	public int Limit { get; init; }
}