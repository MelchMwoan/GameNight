using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain;

namespace GameNight2.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View("Index");
		}

		public IActionResult Privacy()
		{
			return View("Privacy");
		}

		public IActionResult Account()
		{
			return View("Account");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
		}
	}
}