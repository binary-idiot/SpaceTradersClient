using System.Net;

namespace SpaceTraders.Shared.Models.API;

public class ApiResponse<TResult>
{
	public HttpStatusCode StatusCode { get; set; }

	public bool Success => ((int)StatusCode >= 200) && ((int)StatusCode <= 299);
	public bool ClientError => ((int)StatusCode >= 400 && ((int)StatusCode <= 499));

	public TResult? Result { get; set; }
	public object? Error { get; set; }
}