using System.Net.Http.Json;
using SpaceTraders.Shared.Models.Game;

namespace SpaceTraders.Shared.Utilities.Mappers;

public abstract class ModelMapper<TModel> : IModelMapper<TModel>
{
	private record ServerRequest
	{
		public TModel Data { get; init; }
	}
	
	public virtual async Task<ServerData<TModel>> MapToClient(HttpContent content)
	{
		return await content.ReadFromJsonAsync<ServerData<TModel>>();
	}

	public virtual HttpContent MapToServer(TModel model)
	{
		return JsonContent.Create(new ServerRequest(){ Data = model });
	}
}