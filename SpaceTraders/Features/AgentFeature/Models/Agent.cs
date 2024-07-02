using SpaceTraders.Shared.Utilities.Mappers;

namespace SpaceTraders.Features.AgentFeature;

public class AgentModelMapper : ModelMapper<Agent> { }

public class Agent
{
	public string AccountId { get; set; }
	public string Symbol { get; set; }
	public string Headquarters { get; set; }
	public int Credits { get; set; }
	public string StartingFaction { get; set; }
	public int ShipCount { get; set; }
}