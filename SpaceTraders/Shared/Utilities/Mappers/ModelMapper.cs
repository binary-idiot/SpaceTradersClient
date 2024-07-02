using System.Net.Http.Json;

namespace SpaceTraders.Shared.Utilities.Mappers;

public abstract class ModelMapper<TModel> : IModelMapper<TModel>
{
	private record DataWrapper
	{
		public TModel Data { get; init; }
	}
	
	public virtual async Task<TModel?> MapToClient(HttpContent content)
	{
		DataWrapper? data = await content.ReadFromJsonAsync<DataWrapper>();
		return data != null ? data.Data : default;
	}

	public virtual HttpContent MapToServer(TModel model)
	{
		DataWrapper data = new DataWrapper()
		{
			Data = model
		};
		
		return JsonContent.Create(data);
	}
}