using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain;
using DomainServices;
using GameNight2.Models;
using Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SQLData;

namespace GameNight2.Controllers
{
	public class NightController : Controller
	{
		private readonly ILogger<NightController> _logger;
		private INightRepository _nightRepository;
		private UserManager<GameNight2User> _userManager;
		private IAccountRepository _accountRepository;
		private IGameRepository _gameRepository;

		public NightController(ILogger<NightController> logger, INightRepository nightRepository, UserManager<GameNight2User> userManager, IAccountRepository accountRepository, IGameRepository gameRepository)
		{
			_nightRepository = nightRepository;
			_logger = logger;
			_userManager = userManager;
			_accountRepository = accountRepository;
			_gameRepository = gameRepository;
		}

		public IActionResult Nights()
		{
			return View(_nightRepository.getNights());
		}

		public IActionResult NightDetails(int Id)
		{
			var user = _userManager.GetUserAsync(User);
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
		public IActionResult EditNight(int nightId)
		{
			Night night = _nightRepository.getNightById(nightId)?.Night;
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (person == null)
			{
				return RedirectToPage("/Account/Login", new { area = "Identity" });
			}

			EditNightModel editNightModel = new EditNightModel()
			{
				Id = nightId,
				DateTime = night.DateTime,
				MaxPlayers = night.MaxPlayers,
				ThumbnailUrl = night.ThumbnailUrl,
				Title = night.Title
			};
			return View(editNightModel);
		}
		[HttpPost]
		public IActionResult UpdateNight(EditNightModel nightModel)
		{
			if (!ModelState.IsValid) return View("EditNight", nightModel);
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (person == null) return RedirectToPage("/Account/Login", new { area = "Identity" });
			Night? night = _nightRepository.getNightById(nightModel.Id)?.Night;
			if (night == null) throw new Exception("Night doesn't exist");
			if (night.Players.Count > 0) throw new Exception("Can't edit the night while there are attendees");

			int changed = 0;
			foreach (var propertyInfo in nightModel.GetType().GetProperties())
			{
				var modelVal = propertyInfo.GetValue(nightModel);
				var nightVal = night.GetType().GetProperty(propertyInfo.Name).GetValue(night);
				if (!nightVal.Equals(modelVal))
				{
					changed++;
					propertyInfo.SetValue(nightModel, modelVal);
				}
				if (changed > 0) _nightRepository.updateNight(nightModel.getNight());
			}
			return RedirectToAction("NightDetails", "Night", new { id = nightModel.Id});
		}

		public IActionResult RemoveNight(int Id)
		{
			Night? night = _nightRepository.getNightById(Id).Night;
			if (night == null) throw new Exception("Night doesn't exist");
			if (night.Players.Count > 0) throw new Exception("Can't edit the night while there are attendees");
			_nightRepository.removeNight(night);
			return RedirectToAction("Nights", "Night");
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
				AdultOnly = newNight.AdultOnly,
				TakeOwnSnacks = newNight.TakeOwnSnacks,
				PersonId = _accountRepository.getAccount(User.Identity.Name).Id,
				Games = _gameRepository.getGames().Where(x => newNight.SelectedGames.Contains(x.Id)).ToList()
			};
			_nightRepository.addNight(night);
			return RedirectToAction("NightDetails");
		}

		[HttpPost]
		public IActionResult FilterNights(NightsFilterModel filter)
		{
			if(!ModelState.IsValid) return View("Nights", _nightRepository.getNights());
			var nightFilter = new NightFilter
			{
				Name = filter.Name
			};
			var filteredNights = _nightRepository.filterNights(nightFilter);
			return View("Nights", filteredNights);
		}

		public IActionResult GetHostedNights()
		{
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (person == null)
			{
				return RedirectToPage("/Account/Login", new { area = "Identity" });
			}
			return View(_nightRepository.getHostedNights(person.Id));
		}
		public IActionResult GetJoinedNights()
		{
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (person == null)
			{
				return RedirectToPage("/Account/Login", new { area = "Identity" });
			}
			return View(_nightRepository.getJoinedNights(person.Id));
		}

		[HttpPost]
		public IActionResult JoinNight(int nightId)
		{
			if (User.Identity.Name != null)
			{
				_nightRepository.joinNight(nightId, _accountRepository.getAccount(User.Identity.Name));
				return RedirectToAction("NightDetails", "Night", new { id = nightId });
			}
			else
			{
				var returnUrl = Url.Action("NightDetails", "Night", new { id = nightId });
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");

			}

			return NotFound();
		}

		[HttpPost]
		public IActionResult LeaveNight(int nightId)
		{
			if (User.Identity.Name != null)
			{
				_nightRepository.leaveNight(nightId, _accountRepository.getAccount(User.Identity.Name));
				return RedirectToAction("NightDetails", "Night", new { id = nightId });
			}
			else
			{
				var returnUrl = Url.Action("NightDetails", "Night", new { id = nightId });
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
			}

			return NotFound();
		}
	}
}