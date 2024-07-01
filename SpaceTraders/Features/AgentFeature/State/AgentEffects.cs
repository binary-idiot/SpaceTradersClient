using Fluxor;
using SpaceTraders.Shared.Models;
using SpaceTraders.Shared.Models.API;

namespace SpaceTraders.Features.AgentFeature.State;

public class AgentEffects
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public AgentEffects(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	[EffectMethod(typeof(GetAgentAction))]
	public async Task HandleGetAgentAction(IDispatcher dispatcher)
	{
		using IServiceScope scope = _serviceScopeFactory.CreateScope();
		try
		{
			AgentService agentService = scope.ServiceProvider.GetRequiredService<AgentService>();
			ApiResponse<Agent> response = await agentService.GetAgent();

			if (response.Success)
			{
				dispatcher.Dispatch(new GetAgentSuccessAction(response.Result));
			}
			else
			{
				throw new Exception(((Error)response.Error).Message);
			}
		}
		catch(Exception ex)
		{
			ILogger<AgentEffects> logger = scope.ServiceProvider.GetRequiredService<ILogger<AgentEffects>>();
			logger.LogError(ex.ToString());
			dispatcher.Dispatch(new GetAgentFailureAction(ex.Message));
		}
	}
}