using SpaceTraders.Shared.Models;

namespace SpaceTraders.Features.AgentFeature;

public class Agent
{
	public int Credits { get; set; }
	public DateTime JoinedAt { get; set; }
	public int ShipCount { get; set; }
	public int StructureCount { get; set; }
	public string Username { get; set; }
}