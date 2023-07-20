namespace SpaceTraders.Features.StatusFeature;

public class GameStatus
{
	public string Status { get; set; }
	public string Version { get; set; }
	public string ResetDate { get; set; }
	public string Description { get; set; }
	public GameStats Stats { get; set; }
	public Leaderboards Leaderboards { get; set; }
	public ServerResetInfo ServerResetInfo { get; set; }
	public List<Announcment> Announcments { get; set; }
	public List<GameLink> Links { get; set; }

	public bool IsOnline
	{
		get => Status.ToLower() == "spacetraders is currently online and available to play";
	}
}

public class GameStats
{
	public int Agents { get; set; }
	public int Ships { get; set; }
	public int Systems { get; set; }
	public int Waypoints { get; set; }
}

public class Leaderboards
{
	public List<CreditLeaderboard> MostCredits { get; set; }
	public List<CharEnumerator> MostSubmittedCharts { get; set; }
	
}

public class CreditLeaderboard
{
	public string AgentSymbol { get; set; }
	public Int64 Credits { get; set; }
}

public class ChartLeaderboard
{
	public string AgentSymbol { get; set; }
	public int ChartCount { get; set; }
}

public class ServerResetInfo
{
	public string Next { get; set; }
	public string Frequency { get; set; }
}

public class Announcment
{
	public string Title { get; set; }
	public string Body { get; set; }
}

public class GameLink
{
	public string Name { get; set; }
	public string Url { get; set; }
}