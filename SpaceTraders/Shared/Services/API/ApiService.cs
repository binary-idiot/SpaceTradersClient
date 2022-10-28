using System.Net.Http.Json;
using System.Text.Json;
using SpaceTraders.Shared.Models.API;
using SpaceTraders.Shared.Utilities;

namespace SpaceTraders.Shared.Services.API;

public abstract class ApiService<TError> : IApiService where TError : ApiError
{
	protected readonly HttpClient _client;
	protected readonly ILogger _logger;
	private readonly IServiceProvider _serviceProvider;

	public ApiService(HttpClient client, ILogger logger, IServiceProvider serviceProvider)
	{
		_client = client;
		_logger = logger;
		_serviceProvider = serviceProvider;
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
		try
		{
			_logger.LogDebug($"Sending {request.Method} request to: {request.RequestUri}");
			HttpResponseMessage response = await _client.SendAsync(request);

			ApiResponse<TResult> apiResponse = new ApiResponse<TResult>()
			{
				StatusCode = response.StatusCode
			};
		
			if (apiResponse.Success)
			{
				IModelMapper<TResult> mapper = GetMapper<TResult>();
				
				apiResponse.Result = await mapper.MapToClient(response.Content);
				_logger.LogDebug($"Request to {request.RequestUri} successful with code {response.StatusCode}");
				return apiResponse;
			}
		
			if (apiResponse.ClientError)
			{
				apiResponse.Error = await response.Content.ReadFromJsonAsync<TError>();
			}
			_logger.LogWarning($"Error in request to {request.RequestUri}: {(string.IsNullOrWhiteSpace(apiResponse?.Error?.ToString()) ? response.ReasonPhrase : apiResponse.Error)}");
			return apiResponse;
		}
		catch (Exception e)
		{
			_logger.LogError($"Exception thrown in request to {request.RequestUri}: {e.Message}");
			throw;
		}
	}

	protected async Task<HttpRequestMessage> BuildRequest<TBody>(HttpMethod method, ApiQuery<TBody> apiQuery)
	{
		IModelMapper<TBody> mapper = GetMapper<TBody>();
		HttpRequestMessage request = new HttpRequestMessage()
		{
			Method = method,
			RequestUri = new Uri(_client.BaseAddress, (await apiQuery.GetEndpointWithParams())),
			Content = mapper.MapToServer(apiQuery.Body),
		};

		return AddRequestHeaders(request, apiQuery.Headers);
	}
	
	protected async Task<HttpRequestMessage> BuildRequest(HttpMethod method, ApiQuery apiQuery)
	{
		HttpRequestMessage request = new HttpRequestMessage()
		{
			Method = method,
			RequestUri = new Uri(_client.BaseAddress, (await apiQuery.GetEndpointWithParams()))
		};
		
		return AddRequestHeaders(request, apiQuery.Headers);
	}

	protected HttpRequestMessage AddRequestHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
	{
		foreach (var (header, value) in headers)
		{
			request.Headers.Add(header, value);
		}

		return request;
	}

	private IModelMapper<TModel> GetMapper<TModel>()
	{
		Type mapperType = typeof(IModelMapper<>).MakeGenericType(typeof(TModel));
		return (IModelMapper<TModel>)_serviceProvider.GetRequiredService(mapperType);
	}
}