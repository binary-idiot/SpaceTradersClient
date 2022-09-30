using System.Net.Http;
using System.Net.Http.Json;

namespace SpaceTraders.Shared.Services.API;

public abstract class ApiService<TError> : IApiService
{
	protected readonly HttpClient _client;

	public ApiService(HttpClient client)
	{
		_client = client;
	}
	
	public virtual async Task<ApiResponse<TResult>> Get<TResult>(ApiQuery apiQuery)
	{
		return await SendAsync<TResult>(await BuildRequest(new HttpMethod("GET"), apiQuery));
	}

	public virtual async Task<ApiResponse<TResult>> Post<TResult, TBody>(ApiQuery<TBody> apiQuery)
	{
		return await SendAsync<TResult>(await BuildRequest(new HttpMethod("POST"), apiQuery));	
	}

	public virtual async Task<ApiResponse<TResult>> Put<TResult, TBody>(ApiQuery<TBody> apiQuery)
	{
		return await SendAsync<TResult>(await BuildRequest(new HttpMethod("PUT"), apiQuery));	
	}

	public virtual async Task<ApiResponse<TResult>> Patch<TResult, TBody>(ApiQuery<TBody> apiQuery)
	{
		return await SendAsync<TResult>(await BuildRequest(new HttpMethod("PATCH"), apiQuery));	
	}

	public virtual async Task<ApiResponse<TResult>> Delete<TResult>(ApiQuery apiQuery)
	{
		return await SendAsync<TResult>(await BuildRequest(new HttpMethod("DELETE"), apiQuery));	
	}

	protected async Task<ApiResponse<TResult>> SendAsync<TResult>(HttpRequestMessage request)
	{
		HttpResponseMessage response = await _client.SendAsync(request);

		ApiResponse<TResult> apiResponse = new ApiResponse<TResult>()
		{
			StatusCode = response.StatusCode
		};
		
		if (apiResponse.Success)
		{
			apiResponse.Result = await response.Content.ReadFromJsonAsync<TResult>();
		}
		else if (apiResponse.ClientError)
		{
			apiResponse.Error = await response.Content.ReadFromJsonAsync<TError>();
		}

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