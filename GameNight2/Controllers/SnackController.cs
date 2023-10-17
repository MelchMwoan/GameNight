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
		private INightRepository _nightRepository;

		public SnackController(ILogger<SnackController> logger, ISnackRepository snackRepository, UserManager<GameNight2User> userManager, IAccountRepository accountRepository, INightRepository nightRepository)
		{
			_snackRepository = snackRepository;
			_logger = logger;
			_userManager = userManager;
			_accountRepository = accountRepository;
			_nightRepository = nightRepository;
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

		[HttpPost]
		public IActionResult CreateSnack(NewSnackModel snackModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new
				{
					success = false,
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
				});
			}

			Snack snack = snackModel.getSnack();
			snack.person = _accountRepository.getAccount(User.Identity.Name);
			if (snackModel.nightId != null)
			{
				snack.SetNight(_nightRepository.getNightById((int)snackModel.nightId).Night);
			}

			_snackRepository.addSnack(snack);
			return Ok();

		}

		public IActionResult SnackDetails(int Id)
		{
			Snack snack = _snackRepository.getSnackById(Id);
			if (snack != null) return View(snack);
			return RedirectToAction("Index", "Home");
		}
		public IActionResult RemoveSnack(int snackId)
		{
			Snack snack =_snackRepository.getSnackById(snackId);
			_snackRepository.removeSnack(snack);
			return RedirectToAction("NightDetails", "Night", new { id = snack.nightId});
		}
	}
}