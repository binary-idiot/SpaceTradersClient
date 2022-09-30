namespace SpaceTraders.Shared.Services.API;

public class ApiQuery
{
	public string Endpoint { get; set; }
	public Dictionary<string, string> Params { get; set; }

	public async Task<string> BuildQuery()
	{
		using (HttpContent content = new FormUrlEncodedContent(this.Params))
		{
			return $"{this.Endpoint}?{(await content.ReadAsStringAsync())}";
		}
	}
}

public class ApiQuery<TBody> : ApiQuery
{
	public TBody Body { get; set; }
}