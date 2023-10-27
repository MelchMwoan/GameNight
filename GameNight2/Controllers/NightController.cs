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
		private ISnackRepository _snackRepository;

		public NightController(ILogger<NightController> logger, INightRepository nightRepository, UserManager<GameNight2User> userManager, IAccountRepository accountRepository, IGameRepository gameRepository, ISnackRepository snackRepository)
		{
			_nightRepository = nightRepository;
			_logger = logger;
			_userManager = userManager;
			_accountRepository = accountRepository;
			_gameRepository = gameRepository;
			_snackRepository = snackRepository;
		}

		public IActionResult Nights()
		{
			return View("Nights",_nightRepository.getNights());
		}

		public IActionResult NightDetails(int Id)
		{

			if (User.Identity.Name == null)
			{
				var returnUrl = Url.Action("NightDetails", "Night", new { id = Id });
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
			}

			var user = _userManager.GetUserAsync(User);
			NightPersonJoinResult? night = _nightRepository.getNightById(Id);
			if (night == null)
			{
				return RedirectToAction("Index", "Home");
			}
			return View("NightDetails", night);
		}

		[HttpGet]
		public IActionResult CreateNight()
		{
			if (User.Identity.Name == null)
			{
				var returnUrl = Url.Action("CreateNight", "Night");
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
			}
			return View("CreateNight");
		}
		[HttpPost]
		public IActionResult EditNight(int nightId)
		{
			Night night = _nightRepository.getNightById(nightId)?.Night;
			if (User.Identity.Name == null)
			{
				var returnUrl = Url.Action("NightDetails", "Night", new { id = nightId});
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
			}

			EditNightModel editNightModel = new EditNightModel()
			{
				Id = nightId,
				DateTime = night.DateTime,
				MaxPlayers = night.MaxPlayers,
				ThumbnailUrl = night.ThumbnailUrl,
				Title = night.Title,
				Description = night.Description,
				SelectedGames = night.Games.Select(x => x.Id).ToList(),
				SelectedSnacks = night.Snacks.Select(x => x.Id).ToList(),
				AdultOnly = night.AdultOnly,
				TakeOwnSnacks = night.TakeOwnSnacks,
			};
			return View("EditNight", editNightModel);
		}
		[HttpPost]
		public IActionResult UpdateNight(EditNightModel nightModel)
		{
			if (!ModelState.IsValid) return View("EditNight", nightModel);
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (person == null) return RedirectToPage("/Account/Login", new { area = "Identity" });
			NightPersonJoinResult? nightPersonJoinResult = _nightRepository.getNightById(nightModel.Id);
			if (nightPersonJoinResult == null) throw new Exception("Night doesn't exist");
			Night? night = nightPersonJoinResult.Night;
			if (night.Players.Count > 0) throw new Exception("Can't edit the night while there are attendees");

			foreach (var propertyInfo in nightModel.GetType().GetProperties())
			{
				var modelVal = propertyInfo.GetValue(nightModel);
				if (propertyInfo.Name == "SelectedGames" || propertyInfo.Name == "SelectedGames")
				{
					continue;
				}
				var nightVal = night.GetType().GetProperty(propertyInfo.Name)?.GetValue(night);
				if (nightVal != null && !nightVal.Equals(modelVal))
				{
					propertyInfo.SetValue(nightModel, modelVal);
				}
			}
			Night newNight = nightModel.getNight();
			_gameRepository.getGames().Where(x => nightModel.SelectedGames.Contains(x.Id)).ToList().ForEach(game =>
			{
				newNight.AddGame(game);
			});
			_snackRepository.getSnacks().Where(x => nightModel.SelectedSnacks.Contains(x.Id)).ToList().ForEach(snack =>
			{
				newNight.AddSnack(snack);
			});
			_nightRepository.updateNight(newNight);
			return RedirectToAction("NightDetails", "Night", new { id = nightModel.Id});
		}

		public IActionResult RemoveNight(int Id)
		{
			Night? night = _nightRepository.getNightById(Id)?.Night;
			if (night == null) throw new Exception("Night doesn't exist");
			if (night.Players.Count > 0) throw new Exception("Can't remove the night while there are attendees");
			_nightRepository.removeNight(night);
			return RedirectToAction("Nights", "Night");
		}

		[HttpPost]
		public IActionResult CreateNight(NewNightModel newNight)
		{
			if (!ModelState.IsValid) return View("CreateNight", newNight);
			var night = new Night
			{
				Title = newNight.Title,
				Description = newNight.Description,
				DateTime = newNight.DateTime,
				MaxPlayers = newNight.MaxPlayers,
				ThumbnailUrl = newNight.ThumbnailUrl,
				AdultOnly = newNight.AdultOnly,
				TakeOwnSnacks = newNight.TakeOwnSnacks,
				PersonId = _accountRepository.getAccount(User.Identity.Name).Id
			};
			_gameRepository.getGames().Where(x => newNight.SelectedGames.Contains(x.Id)).ToList().ForEach(game =>
			{
				night.AddGame(game);
			});
			_snackRepository.getSnacks().Where(x => newNight.SelectedSnacks.Contains(x.Id)).ToList().ForEach(snack =>
			{
				night.AddSnack(snack);
			});
			_nightRepository.addNight(night);
			newNight.SelectedSnacks.ForEach(snackId =>
			{
				_snackRepository.setSnackNight(snackId, night);
			});
			return RedirectToAction("NightDetails", new { id = night.Id});
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
			if (User.Identity.Name == null)
			{
				var returnUrl = Url.Action("GetHostedNights", "Night");
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
			}
			return View("GetHostedNights", _nightRepository.getHostedNights(person.Id));
		}
		public IActionResult GetJoinedNights()
		{
			Person person = _accountRepository.getAccount(User.Identity.Name);
			if (User.Identity.Name == null)
			{
				var returnUrl = Url.Action("GetJoinedNights", "Night");
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
			}
			return View("GetJoinedNights", _nightRepository.getJoinedNights(person.Id));
		}

		[HttpPost]
		public IActionResult JoinNight(int nightId)
		{
			if (User.Identity.Name != null)
			{
				Person person = _accountRepository.getAccount(User.Identity.Name);
				Night? night = _nightRepository.getNightById(nightId)?.Night;
				if (night == null) throw new Exception("Night doesn't exist");
				List<Night> joinedNights = _nightRepository.getJoinedNights(person.Id);
				if (joinedNights.Any(x => x.DateTime.Date == night.DateTime.Date))
					throw new Exception("Can't join 2 nights on the same day");
				_nightRepository.joinNight(nightId, person);
				return RedirectToAction("NightDetails", "Night", new { id = nightId });
			}
			else
			{
				var returnUrl = Url.Action("NightDetails", "Night", new { id = nightId });
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");

			}
		}

		[HttpPost]
		public IActionResult LeaveNight(int nightId)
		{
			if (User.Identity.Name != null)
			{
				Night? night = _nightRepository.getNightById(nightId)?.Night;
				if (night == null) throw new Exception("Night doesn't exist");
				_nightRepository.leaveNight(nightId, _accountRepository.getAccount(User.Identity.Name));
				return RedirectToAction("NightDetails", "Night", new { id = nightId });
			}
			else
			{
				var returnUrl = Url.Action("NightDetails", "Night", new { id = nightId });
				return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
			}
		}
	}
}