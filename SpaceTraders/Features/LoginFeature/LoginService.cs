using Blazored.LocalStorage;
using SpaceTraders.Shared.Services;

namespace SpaceTraders.Features.LoginFeature;

public class LoginService : IDataService
{
	private readonly string key = "login";
	private readonly ILocalStorageService _localStorage;

	public LoginService(ILocalStorageService localStorage)
	{
		_localStorage = localStorage;
	}
	
	public async Task<Login> GetSavedLogin()
	{
		return await _localStorage.GetItemAsync<Login>(key);
	}

	public async Task SetSavedLogin(Login login)
	{
		await _localStorage.SetItemAsync(key, login);
	}
}