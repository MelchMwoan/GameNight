
using Domain;
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
			var night = _nightRepository.getNightById(id)?.Night;
			try
			{
				if(night == null)
					throw new Exception("Night doesn't exist");
				List<Night> joinedNights = _nightRepository.getJoinedNights(person.Id);
				if (joinedNights.Any(x => x.DateTime.Date == night.DateTime.Date))
					throw new Exception("Can't join 2 nights on the same day");
				if (night.Players.Count >= night.MaxPlayers)
					throw new Exception("This Night is already full");
				if (night.AdultOnly && (DateTime.Now.AddYears(-18) < person.BirthDate))
					throw new Exception("This Night is for adults only");
				if (night.TakeOwnSnacks && !night.Snacks.Any(x => x.personId == person.Id))
					throw new Exception("The Organisator of this night wants every attendee to bring a snack and you haven't submitted a snack yet");
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