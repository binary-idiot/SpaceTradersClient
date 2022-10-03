using SpaceTraders.Models.API;

namespace SpaceTraders.Models.Game;

public class Error : ApiError
{
	public string Message { get; set; }
	public int Code { get; set; }
	public override string ToString()
	{
		return Message;
	}
}