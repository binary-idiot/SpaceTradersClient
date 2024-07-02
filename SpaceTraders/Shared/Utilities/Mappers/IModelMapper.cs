using SpaceTraders.Shared.Models.Game;

namespace SpaceTraders.Shared.Utilities.Mappers;
public interface IModelMapper {}
public interface IModelMapper<TModel> : IModelMapper
{
	public Task<ServerData<TModel>> MapToClient(HttpContent content);
	public HttpContent MapToServer(TModel model);
}