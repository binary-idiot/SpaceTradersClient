using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpaceTraders.Shared.Models.Game;

[JsonConverter(typeof(LocationJsonConverter))]
public class Location
{
	public string Sector { get; }
	public string System { get; }
	public string Waypoint { get; }

	public string FullSystem
	{
		get => $"{Sector}{PrefixPart(System)}";
	}
	
	public string FullWaypoint
	{
		get => $"{FullSystem}{PrefixPart(Waypoint)}";
	}

	public Location(string? location)
	{
		location ??= string.Empty;
		string[] parts = location.Split('-');
		Sector = (parts.Length > 0) ? parts[0] : String.Empty;
		System = (parts.Length > 1) ? parts[1] : String.Empty;
		Waypoint = (parts.Length > 2) ? parts[2] : String.Empty;
	}

	public static implicit operator string(Location l) => l.FullWaypoint;
	public static implicit operator Location(string l) => new Location(l);
	public override string ToString() => FullWaypoint;

	private static string PrefixPart(string part)
	{
		if (string.IsNullOrWhiteSpace(part))
		{
			return String.Empty;
		}

		return $"-{part}";
	}
}

public class LocationJsonConverter : JsonConverter<Location>
{
	public override Location? Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => new Location(reader.GetString());

	public override void Write(
		Utf8JsonWriter writer,
		Location value,
		JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
}