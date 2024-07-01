using Fluxor;
using SpaceTraders.Shared.State;

namespace SpaceTraders.Features.AgentFeature.State;

[FeatureState]
public class AgentState : RootState
{
	public Agent? Agent { get; }

	public AgentState() {}

	public AgentState(Agent? agent, bool isLoading = false, string? currentErrorMessage = null) 
		: base(isLoading, currentErrorMessage)
	{
		Agent = agent;
	}
}