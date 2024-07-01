using System.Net.Http.Json;
using SpaceTraders.Shared.Utilities;
using SpaceTraders.Shared.Utilities.Mappers;

namespace SpaceTraders.Features.AgentFeature;

public class AgentModelMapper : ModelMapper<Agent>
{
	private record ServerAgent
	{
		public Agent User { get; init; }	
	}

	public override async Task<Agent?> MapToClient(HttpContent content)
	{
		ServerAgent? server = await content.ReadFromJsonAsync<ServerAgent>();
		return server?.User;
	}

	public override HttpContent MapToServer(Agent model)
	{
		return JsonContent.Create(new ServerAgent(){ User = model });
	}
}