using System.ComponentModel.DataAnnotations;

namespace SpaceTraders.Features.LoginFeature;

public class Login
{
	[Required]
	public string? Username { get; set; }
	[Required]
	public string? Token { get; set; }
}