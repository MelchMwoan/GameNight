
using DomainServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameNightAPI.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	[Route("api/[controller]")]
	public class NightController : ControllerBase
	{
		private readonly ILogger<NightController> _logger;
		private readonly INightRepository _nightRepository;
		private readonly IAccountRepository _accountRepository;

		public NightController(ILogger<NightController> logger, INightRepository nightRepository, IAccountRepository accountRepository)
		{
			_logger = logger;
			_nightRepository = nightRepository;
			_accountRepository = accountRepository;
		}

		[HttpPost("{id}/Join"), Authorize]
		public IActionResult JoinNight(int id)
		{
			var person = _accountRepository.getAccount(User.Identity.Name);
			try
			{
				_nightRepository.joinNight(id, person);
			}
			catch (Exception e)
			{
				return BadRequest(new
				{
					success = false,
					message = e.Message
				});
			}
			return Ok(new
			{
				succes = true,
				message = $"{person.Name} succesfully joined night with id {id}"
			});
		}
	}
}