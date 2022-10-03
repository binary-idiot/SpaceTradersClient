namespace SpaceTraders.Models.API;

public class ApiQuery
{
	private string _endpoint;
	public string Endpoint
	{
		get => _endpoint;
		set => _endpoint = (value.Length > 0 && value.Substring(0, 1) != "/")
			? $"/{value}" : value;
	}

	public Dictionary<string, string> Params { get; set; }

	public ApiQuery()
	{
		_endpoint = String.Empty;
		Params = new Dictionary<string, string>();
	}
	public async Task<string> BuildQuery()
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