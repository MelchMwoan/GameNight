using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain;
using GameNightAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GameNightAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : ControllerBase
	{
		private readonly ILogger<AuthenticationController> _logger;
		private readonly UserManager<GameNight2User> _userManager;
		private readonly SignInManager<GameNight2User> _signInManager;
		private readonly IConfiguration _config;

		public AuthenticationController(ILogger<AuthenticationController> logger, UserManager<GameNight2User> userManager, SignInManager<GameNight2User> signInManager, IConfiguration config)
		{
			_logger = logger;
			_userManager = userManager;
			_signInManager = signInManager;
			_config = config;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> SignIn([FromBody] AuthenticationCredentials authenticationCredentials)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _userManager.FindByEmailAsync(authenticationCredentials.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, authenticationCredentials.Password))
			{
				return Unauthorized();
			}

			var secTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
				Expires = DateTime.Now.AddDays(int.Parse(_config["BearerTokens:ExpiryDays"])),
				SigningCredentials =
					new SigningCredentials(
						new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["BearerTokens:Key"])),
						SecurityAlgorithms.HmacSha256Signature)
			};

			var handler = new JwtSecurityTokenHandler();
			var secToken = new JwtSecurityTokenHandler().CreateToken(secTokenDescriptor);

			return Ok(new { Succes = true, Token = handler.WriteToken(secToken), Expiration = secToken.ValidTo });
		}

		[HttpPost("Logout")]
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return Ok();
		}
	}
}