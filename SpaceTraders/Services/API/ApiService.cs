using System.Net.Http.Json;
using SpaceTraders.Models.API;

namespace SpaceTraders.Services.API;

public abstract class ApiService<TError> : IApiService where TError : ApiError
{
	protected readonly HttpClient _client;
	protected readonly ILogger _logger;

	public ApiService(HttpClient client, ILogger logger)
	{
		_client = client;
		_logger = logger;
	}
	
	public virtual async Task<ApiResponse<TResult>> Get<TResult>(ApiQuery apiQuery)
	{
		return await Send<TResult>(await BuildRequest(new HttpMethod("GET"), apiQuery));
	}

	public virtual async Task<ApiResponse<TResult>> Post<TResult, TBody>(ApiQuery<TBody> apiQuery)
	{
		return await Send<TResult>(await BuildRequest(new HttpMethod("POST"), apiQuery));	
	}

	public virtual async Task<ApiResponse<TResult>> Put<TResult, TBody>(ApiQuery<TBody> apiQuery)
	{
		return await Send<TResult>(await BuildRequest(new HttpMethod("PUT"), apiQuery));	
	}

	public virtual async Task<ApiResponse<TResult>> Patch<TResult, TBody>(ApiQuery<TBody> apiQuery)
	{
		return await Send<TResult>(await BuildRequest(new HttpMethod("PATCH"), apiQuery));	
	}

	public virtual async Task<ApiResponse<TResult>> Delete<TResult>(ApiQuery apiQuery)
	{
		return await Send<TResult>(await BuildRequest(new HttpMethod("DELETE"), apiQuery));	
	}

	protected async Task<ApiResponse<TResult>> Send<TResult>(HttpRequestMessage request)
	{
		_logger.LogDebug($"Sending {request.Method} request to: {request.RequestUri}");
		HttpResponseMessage response = await _client.SendAsync(request);

		ApiResponse<TResult> apiResponse = new ApiResponse<TResult>()
		{
			StatusCode = response.StatusCode
		};
		
		if (apiResponse.Success)
		{
			apiResponse.Result = await response.Content.ReadFromJsonAsync<TResult>();
			_logger.LogDebug($"Request to {request.RequestUri} successful with code {response.StatusCode}");
			return apiResponse;
		}
		
		if (apiResponse.ClientError)
		{
			apiResponse.Error = await response.Content.ReadFromJsonAsync<TError>();
		}
		_logger.LogWarning($"Error in request to {request.RequestUri}: {(string.IsNullOrWhiteSpace(apiResponse.Error.ToString()) ? response.ReasonPhrase : apiResponse.Error)}");
		return apiResponse;
	}

	protected async Task<HttpRequestMessage> BuildRequest<TResult>(HttpMethod method, ApiQuery<TResult> apiQuery)
	{
		 return new HttpRequestMessage()
		{
			Method = method,
			RequestUri = new Uri(_client.BaseAddress, (await apiQuery.BuildQuery())),
			Content = JsonContent.Create(apiQuery.Body)
		};
	}
	
	protected async Task<HttpRequestMessage> BuildRequest(HttpMethod method, ApiQuery apiQuery)
	{
		return new HttpRequestMessage()
		{
			Method = method,
			RequestUri = new Uri(_client.BaseAddress, (await apiQuery.BuildQuery()))
		};
	}
}