using Microsoft.Build.Framework;

namespace GameNightAPI.Models
{

	public class AuthenticationCredentials
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

	}
}
