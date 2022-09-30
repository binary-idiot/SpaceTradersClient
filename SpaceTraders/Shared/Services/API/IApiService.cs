namespace SpaceTraders.Shared.Services.API;

public interface IApiService
{
	public Task<ApiResponse<TResult>> Get<TResult>(ApiQuery apiQuery);
	public Task<ApiResponse<TResult>> Post<TResult, TBody>(ApiQuery<TBody> apiQuery);
	public Task<ApiResponse<TResult>> Put<TResult, TBody>(ApiQuery<TBody> apiQuery);
	public Task<ApiResponse<TResult>> Patch<TResult, TBody>(ApiQuery<TBody> apiQuery);
	public Task<ApiResponse<TResult>> Delete<TResult>(ApiQuery apiQuery);
}