using System.Net.Http.Json;

namespace SpaceTraders.Shared.Utilities;

public abstract class ModelMapper<TModel> : IModelMapper<TModel>
{
	public virtual async Task<TModel?> MapToClient(HttpContent content)
	{
		return await content.ReadFromJsonAsync<TModel>();
	}

	public virtual HttpContent MapToServer(TModel model)
	{
		return JsonContent.Create(model);
	}
}