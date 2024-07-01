using Fluxor;

namespace SpaceTraders.Features.AgentFeature.State;

public class AgentReducers
{
	[ReducerMethod(typeof(GetAgentAction))]
	public static AgentState ReduceGetAgentAction(AgentState state) =>
		new AgentState(
			agent: state.Agent ?? new Agent(),
			isLoading: true,
			currentErrorMessage: null
		);

	[ReducerMethod]
	public static AgentState ReduceGetAgentSuccessAction(AgentState state, GetAgentSuccessAction action) =>
		new AgentState(
			agent: action.Agent,
			isLoading: false,
			currentErrorMessage: null
		);
	
	[ReducerMethod]
	public static AgentState ReduceGetAgentFailureAction(AgentState state, GetAgentFailureAction action) =>
		new AgentState(
			agent: state.Agent,
			isLoading: false,
			currentErrorMessage: action.ErrorMessage
		);
}