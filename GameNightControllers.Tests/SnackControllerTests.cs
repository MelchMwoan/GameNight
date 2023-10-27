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
	public class SnackControllerTests
	{
		private SnackController _sut;
		private ILogger<SnackController> _logger = Substitute.For<ILogger<SnackController>>();
		private ISnackRepository _snackRepository = Substitute.For<ISnackRepository>();
		private UserManager<GameNight2User> _userManager = Substitute.For<UserManager<GameNight2User>>(Substitute.For<IUserStore<GameNight2User>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<GameNight2User>>(), Substitute.For<IEnumerable<IUserValidator<GameNight2User>>>(), Substitute.For<IEnumerable<IPasswordValidator<GameNight2User>>>(), Substitute.For<ILookupNormalizer>(), Substitute.For<IdentityErrorDescriber>(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<GameNight2User>>>());
		private IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
		private INightRepository _nightRepository = Substitute.For<INightRepository>();
		public SnackControllerTests()
		{
			_sut = new SnackController(_logger, _snackRepository, _userManager, _accountRepository, _nightRepository);
		}

		[Fact]
		public void GetListOfSnacks()
		{
			List<Snack> listSnacks = new List<Snack>
			{
				new Snack { Id = 1 },
				new Snack { Id = 2 },
				new Snack { Id = 3 },
			};
			_snackRepository.getSnacks().Returns(listSnacks);

			var returnedListSnacks = _sut.GetSnacks();

			Assert.NotNull(returnedListSnacks);
			Assert.Equal(listSnacks, returnedListSnacks);
		}

		[Fact]
		public void CreateSnackGetViewReturned()
		{
			const string expectedViewName = "CreateSnack";

			var actualViewName = (_sut.CreateSnack() as ViewResult)?.ViewName;

			Assert.Equal(expectedViewName, actualViewName);
		}


		[Fact]
		public void CreateSnackPostCorrectModelReturnedOk()
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
			_accountRepository.getAccount("TestUser").Returns(new Person());
			var validSnackModel = new NewSnackModel
			{
				Name = "Test Snack"
			};

			var result = _sut.CreateSnack(validSnackModel) as IActionResult;

			Assert.NotNull(result);
			Assert.IsType<OkResult>(result);
			_snackRepository.Received(1).addSnack(Arg.Is<Snack>(g => g.Name == "Test Snack"));
		}

		[Fact]
		public void CreateSnackPostCorrectModelReturnedOkWithSnackSetNight()
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
			_accountRepository.getAccount("TestUser").Returns(new Person());
			var validSnackModel = new NewSnackModel
			{
				Name = "Test Snack",
				nightId = 1
			};
			Night expectedNight = new Night{Id =1};
			_nightRepository.getNightById(1).Returns(new NightPersonJoinResult { Night = expectedNight });

			var result = _sut.CreateSnack(validSnackModel) as IActionResult;

			Assert.NotNull(result);
			Assert.IsType<OkResult>(result);
			_snackRepository.Received(1).addSnack(Arg.Is<Snack>(g => g.Name == "Test Snack" && g.night == expectedNight && g.nightId == expectedNight.Id));
		}

		[Fact]
		public void CreateSnackPostFalseModelReturnedBadRequest()
		{
			var invalidSnackModel = new NewSnackModel();
			_sut.ModelState.AddModelError("PropertyName", "Error Message");

			var result = _sut.CreateSnack(invalidSnackModel);

			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			_snackRepository.DidNotReceive().addSnack(Arg.Any<Snack>());
			Assert.False((bool)badRequestResult.Value?.GetType().GetProperty("success")?.GetValue(badRequestResult.Value)!);
		}


		[Fact]
		public void SnackDetailsViewReturnedWithValidSnack()
		{
			const string expectedViewName = "SnackDetails";
			Snack snack = new Snack { Id = 1 };
			_snackRepository.getSnackById(1).Returns(snack);

			var result = _sut.SnackDetails(1) as ViewResult;

			Assert.Equal(expectedViewName, result?.ViewName);
			Assert.NotNull(result?.Model);
			Assert.IsType<Snack>(result?.Model);
			Assert.Equal(snack, result?.Model);
		}


		[Fact]
		public void IndexRedirectWithInvalidSnack()
		{
			const string expectedViewName = "Index";
			const string expectedControllerName = "Home";

			var result = _sut.SnackDetails(2) as RedirectToActionResult;

			Assert.Equal(expectedViewName, result?.ActionName);
			Assert.Equal(expectedControllerName, result?.ControllerName);
		}

		[Fact]
		public void RemoveSnackWithFalseSnack()
		{
			_snackRepository.getSnackById(1).Returns((Snack?)null);

			var result = _sut.RemoveSnack(1) as IActionResult;

			Assert.NotNull(result);
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public void RemoveSnackWithValidSnack()
		{
			Snack snack = new Snack { nightId = 1 };
			_snackRepository.getSnackById(1).Returns(snack);

			var result = _sut.RemoveSnack(1) as RedirectToActionResult;

			Assert.Equal("NightDetails", result?.ActionName);
			Assert.Equal("Night", result?.ControllerName);
			Assert.Equal(1, result?.RouteValues?["id"]);
		}

	}
}