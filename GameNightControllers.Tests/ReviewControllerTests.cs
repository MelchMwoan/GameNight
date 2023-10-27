using Domain;
using DomainServices;
using GameNight2.Controllers;
using GameNight2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Security.Claims;
using Infrastructure.EF;
using Xunit;

namespace GameNightControllers.Tests
{
	public class ReviewControllerTests
	{
		private ReviewController _sut;
		private ILogger<ReviewController> _logger = Substitute.For<ILogger<ReviewController>>();
		private IReviewRepository _reviewRepository = Substitute.For<IReviewRepository>();
		private UserManager<GameNight2User> _userManager = Substitute.For<UserManager<GameNight2User>>(Substitute.For<IUserStore<GameNight2User>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<GameNight2User>>(), Substitute.For<IEnumerable<IUserValidator<GameNight2User>>>(), Substitute.For<IEnumerable<IPasswordValidator<GameNight2User>>>(), Substitute.For<ILookupNormalizer>(), Substitute.For<IdentityErrorDescriber>(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<GameNight2User>>>());
		private IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
		private INightRepository _nightRepository = Substitute.For<INightRepository>();
		public ReviewControllerTests()
		{
			_sut = new ReviewController(_logger, _reviewRepository, _userManager, _accountRepository, _nightRepository);
		}

		[Fact]
		public void GetListOfReviews()
		{
			List<Review> listReviews = new List<Review>
			{
				new Review { Id = 1 },
				new Review { Id = 2 },
				new Review { Id = 3 },
			};
			_reviewRepository.GetReviews().Returns(listReviews);

			var returnedListReviews = _sut.GetReviews();

			Assert.NotNull(returnedListReviews);
			Assert.Equal(listReviews, returnedListReviews);
		}

		[Fact]
		public void CreateReviewGetViewReturned()
		{
			const string expectedViewName = "CreateReview";

			var actualViewName = (_sut.CreateReview() as ViewResult)?.ViewName;

			Assert.Equal(expectedViewName, actualViewName);
		}


		[Fact]
		public void CreateReviewPostCorrectModelReturnedOk()
		{
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, "TestUser")
					}))
				}
			};
			Person writer = new Person();
			_accountRepository.getAccount("TestUser").Returns(writer);
			Person organisator = new Person();
			_nightRepository.getNightById(1).Returns(new NightPersonJoinResult{Night = new Night{Id = 1, Organisator = organisator}});
			var validReviewModel = new NewReviewModel
			{
				Description = "Review for test",
				nightId = 1
			};

			var result = _sut.CreateReview(validReviewModel) as IActionResult;

			Assert.NotNull(result);
			Assert.IsType<OkResult>(result);
			_reviewRepository.Received(1).AddReview(Arg.Is<Review>(g => g.Description == "Review for test" && g.Organisator == organisator && g.Writer == writer));
		}

		[Fact]
		public void CreateReviewPostFalseModelReturnedBadRequest()
		{
			var invalidReviewModel = new NewReviewModel();
			_sut.ModelState.AddModelError("PropertyName", "Error Message");

			var result = _sut.CreateReview(invalidReviewModel);

			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			_reviewRepository.DidNotReceive().AddReview(Arg.Any<Review>());
			Assert.False((bool)badRequestResult.Value?.GetType().GetProperty("success")?.GetValue(badRequestResult.Value)!);
		}

		[Fact]
		public void RemoveReviewWithFalseReview()
		{
			_reviewRepository.GetReviewById(1).Returns((Review?)null);

			var result = _sut.RemoveReview(1) as IActionResult;

			Assert.NotNull(result);
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public void RemoveSnackWithValidSnack()
		{
			Review review = new Review { nightId = 1 };
			_reviewRepository.GetReviewById(1).Returns(review);

			var result = _sut.RemoveReview(1) as RedirectToActionResult;

			Assert.Equal("NightDetails", result?.ActionName);
			Assert.Equal("Night", result?.ControllerName);
			Assert.Equal(1, result?.RouteValues?["id"]);
		}

	}
}