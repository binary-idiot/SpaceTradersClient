using SpaceTraders.Shared.State;

namespace SpaceTraders.Features.AgentFeature.State;

public abstract class BaseAgentAction : IAction
{
	public Agent Agent { get; }

	public BaseAgentAction(Agent agent)
	{
		Agent = agent;
	}
}

public class GetAgentAction : IAction {}

public class GetAgentSuccessAction : BaseAgentAction
{
	public GetAgentSuccessAction(Agent agent) : base(agent) { }
}

public class GetAgentFailureAction : FailureAction
{
	public GetAgentFailureAction(string errorMessage) : base(errorMessage) { }
}