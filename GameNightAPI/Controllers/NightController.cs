using Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace GameNightAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class NightController : ControllerBase
	{
		private readonly ILogger<NightController> _logger;
		private INightRepository _nightRepository;
		private IGameRepository _gameRepository;

		public NightController(ILogger<NightController> logger, INightRepository nightRepository, IGameRepository gameRepository)
		{
			_logger = logger;
			_nightRepository = nightRepository;
			_gameRepository = gameRepository;
		}

		[HttpGet(Name = "GetNights")]
		public IEnumerable<Night> GetAllNights()
		{
			IEnumerable<Night> nights = _nightRepository.getNights().ToArray();
			foreach (Night night in nights)
			{
				night.Games.ForEach(game => game.Nights = null);
				night.Organisator.Nights = null;
				night.Players.ForEach(player => player.Nights = null);
			}
			return _nightRepository.getNights().ToArray();
			//return _nightRepository.getNights().ToArray().Join(_gameRepository.getGames());
		}
	}
}