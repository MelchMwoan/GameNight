using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain;
using GameNight2.Models;
using DomainServices;

namespace GameNight2.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		private IAccountRepository _accountRepository;

		public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
			_logger = logger;
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
		}
	}
}