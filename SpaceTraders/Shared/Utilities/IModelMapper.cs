namespace SpaceTraders.Shared.Utilities;
public interface IModelMapper {}
public interface IModelMapper<TModel> : IModelMapper
{
	public Task<TModel?> MapToClient(HttpContent content);
	public HttpContent MapToServer(TModel model);
}