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
	public class ReviewController : Controller
	{
		private readonly ILogger<ReviewController> _logger;
		private IReviewRepository _reviewRepository;
		private UserManager<GameNight2User> _userManager;
		private IAccountRepository _accountRepository;
		private INightRepository _nightRepository;

		public ReviewController(ILogger<ReviewController> logger, IReviewRepository reviewRepository, UserManager<GameNight2User> userManager, IAccountRepository accountRepository, INightRepository nightRepository)
		{
			_reviewRepository = reviewRepository;
			_logger = logger;
			_userManager = userManager;
			_accountRepository = accountRepository;
			_nightRepository = nightRepository;
		}

		public List<Review> GetReviews()
		{
			return _reviewRepository.GetReviews();
		}

		[HttpGet]
		public IActionResult CreateReview()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateReview(NewReviewModel reviewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new
				{
					success = false,
					errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
				});
			}

			Review review = reviewModel.GetReview();
			review.setWriter(_accountRepository.getAccount(User.Identity.Name));
			review.setNight(_nightRepository.getNightById(reviewModel.nightId).Night);
			review.setOrganisator(review.night.Organisator);
			Console.WriteLine(review.Rating);
			_reviewRepository.AddReview(review);
			return Ok();

		}

		public IActionResult RemoveReview(int reviewId)
		{
			Review review = _reviewRepository.GetReviewById(reviewId);
			_reviewRepository.RemoveReview(review);
			return RedirectToAction("NightDetails", "Night", new { id = review.nightId});
		}
	}
}