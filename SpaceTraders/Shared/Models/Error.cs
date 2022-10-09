using SpaceTraders.Shared.Models.API;

namespace SpaceTraders.Shared.Models;

public class Error : ApiError
{
	public string Message { get; set; }
	public int Code { get; set; }
	public override string ToString()
	{
		return Message;
	}
}