using System.Net.Http.Json;
using SpaceTraders.Shared.Utilities;
using SpaceTraders.Shared.Utilities.Mappers;

namespace SpaceTraders.Features.AccountFeature;

public class AccountModelMapper : ModelMapper<Account>
{
	private record ServerAccount
	{
		public Account User { get; init; }	
	}

	public override async Task<Account?> MapToClient(HttpContent content)
	{
		ServerAccount? server = await content.ReadFromJsonAsync<ServerAccount>();
		return server?.User;
	}

	public override HttpContent MapToServer(Account model)
	{
		return JsonContent.Create(new ServerAccount(){ User = model });
	}
}