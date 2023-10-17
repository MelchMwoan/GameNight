using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain;
using DomainServices;
using GameNight2.Models;
using Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLData;

namespace GameNight2.Controllers
{
	public class SnackController : Controller
	{
		private readonly ILogger<SnackController> _logger;
		private ISnackRepository _snackRepository;
		private UserManager<GameNight2User> _userManager;
		private IAccountRepository _accountRepository;

		public SnackController(ILogger<SnackController> logger, ISnackRepository snackRepository, UserManager<GameNight2User> userManager, IAccountRepository accountRepository)
		{
			_snackRepository = snackRepository;
			_logger = logger;
			_userManager = userManager;
			_accountRepository = accountRepository;
		}

		public List<Snack> GetSnacks()
		{
			return _snackRepository.getSnacks();
		}

		[HttpGet]
		public IActionResult CreateSnack()
		{
			return View();
		}

		//[HttpPost]
		//public IActionResult CreateGame(NewGameModel gameModel)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
		//	};
		//	Game game = gameModel.getGame();
		//	_gameRepository.addGame(game);
		//	return Ok();
		//}

		public IActionResult SnackDetails(int Id)
		{
			Snack snack = _snackRepository.getSnackById(Id);
			if(snack != null) return View(snack);
			return RedirectToAction("Index", "Home");
		}
	}
}