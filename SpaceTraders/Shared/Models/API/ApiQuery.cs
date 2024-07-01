namespace SpaceTraders.Shared.Models.API;

public class ApiQuery
{
	private string _endpoint = String.Empty;
	public string Endpoint { get; set; } = "";

	public Dictionary<string, string> Params { get; set; } = new();
	public Dictionary<string, string> Headers { get; set; } = new();
	public string? Authorization { get; set; } = null;

	public async Task<string> GetEndpointWithParams()
	{
		if (Params.Count < 1)
		{
			return Endpoint;
		}

		using HttpContent content = new FormUrlEncodedContent(Params);
		return $"{Endpoint}?{(await content.ReadAsStringAsync())}";
	}
}

public class ApiQuery<TBody> : ApiQuery
{
	public TBody Body { get; set; }
}