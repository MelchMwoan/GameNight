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
	public class NightController : Controller
	{
		private readonly ILogger<NightController> _logger;
		private INightRepository _nightRepository;
		private UserManager<GameNight2User> _userManager;
		private IAccountRepository _accountRepository;

		public NightController(ILogger<NightController> logger, INightRepository nightRepository, UserManager<GameNight2User> userManager, IAccountRepository accountRepository)
		{
			_nightRepository = nightRepository;
			_logger = logger;
			_userManager = userManager;
			_accountRepository = accountRepository;
		}

		public IActionResult Nights()
		{
			return View(_nightRepository.getNights());
		}

		public IActionResult NightDetails(int Id)
		{
			var user = _userManager.GetUserAsync(User);
			Console.WriteLine(user);
			NightPersonJoinResult? night = _nightRepository.getNightById(Id);
			if (night == null)
			{
				return RedirectToAction("Index", "Home");
			}
			return View(night);
		}

		[HttpGet]
		public IActionResult CreateNight()
		{
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (person == null)
			{
				return RedirectToPage("/Account/Login", new { area = "Identity" });
			}
			return View();
		}

		[HttpPost]
		public IActionResult CreateNight(NewNightModel newNight)
		{
			if (!ModelState.IsValid) return View(newNight);

			var night = new Night
			{
				Title = newNight.Title, 
				DateTime = newNight.DateTime, 
				MaxPlayers = newNight.MaxPlayers, 
				ThumbnailUrl = newNight.ThumbnailUrl,
				PersonId = _accountRepository.getAccount(User.Identity.Name).Id
		};
			_nightRepository.addNight(night);
			return RedirectToAction("NightDetails");
		}

		public IActionResult GetHostedNights()
		{
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (person == null)
			{
				return RedirectToPage("/Account/Login", new { area = "Identity" });
			}
			Console.WriteLine(person.Id);
			return View(_nightRepository.getHostedNights(person.Id));
		}

		public void JoinNight([FromBody] NightPersonJoinResult nightPersonJoinResult)
		{
			if (User.Identity.Name != null)
			{
				_nightRepository.joinNight(nightPersonJoinResult.Night.Id, _accountRepository.getAccount(User.Identity.Name));
			}
		}
	}
}