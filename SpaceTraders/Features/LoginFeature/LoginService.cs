using Blazored.LocalStorage;
using SpaceTraders.Shared.Services;

namespace SpaceTraders.Features.LoginFeature;

public class LoginService : IDataService
{
	private readonly string key = "login";
	private readonly ILocalStorageService _localStorage;
	private readonly ILogger _logger;

	public LoginService(ILocalStorageService localStorage, ILogger<LoginService> logger)
	{
		_localStorage = localStorage;
		_logger = logger;
	}
	
	public async Task<Login> GetSavedLogin()
	{
		Login login = await _localStorage.GetItemAsync<Login>(key) ?? new Login();
		if (string.IsNullOrWhiteSpace(login?.Username) && string.IsNullOrWhiteSpace(login?.Token))
		{
			_logger.LogInformation($"No saved login found");
		}
		else
		{
			_logger.LogInformation($"Loaded login (Username: {login.Username}, Token: {login.Token})");
		}
		return login;
	}

	public async Task SetSavedLogin(Login login)
	{
		_logger.LogInformation($"Saving login (Username: {login.Username}, Token: {login.Token})");
		await _localStorage.SetItemAsync(key, login);
	}
}