using Domain;
using DomainServices;
using GameNight2.Controllers;
using GameNight2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace GameNightControllers.Tests
{
	public class GameControllerTests
	{
		private GameController _sut;
		private ILogger<GameController> _logger = Substitute.For<ILogger<GameController>>();
		private IGameRepository _gameRepository = Substitute.For<IGameRepository>();
		private UserManager<GameNight2User> _userManager = Substitute.For<UserManager<GameNight2User>>(Substitute.For<IUserStore<GameNight2User>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<GameNight2User>>(), Substitute.For<IEnumerable<IUserValidator<GameNight2User>>>(), Substitute.For<IEnumerable<IPasswordValidator<GameNight2User>>>(), Substitute.For<ILookupNormalizer>(), Substitute.For<IdentityErrorDescriber>(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<GameNight2User>>>());
		private IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
		public GameControllerTests()
		{
			_sut = new GameController(_logger, _gameRepository, _userManager, _accountRepository);
		}

		[Fact]
		public void GetListOfGames()
		{
			List<Game> listGames = new List<Game>
			{
				new Game { Id = 1, },
				new Game { Id = 2, },
				new Game { Id = 3, },
			};
			_gameRepository.getGames().Returns(listGames);

			var returnedListGames = _sut.GetGames();

			Assert.Equal(listGames, returnedListGames);
		}

		[Fact]
		public void CreateGameGetViewAndModelReturned()
		{
			const string expectedViewName = "CreateGame";

			var actualViewName = (_sut.CreateGame() as ViewResult)?.ViewName;
			var errorViewModel = (_sut.CreateGame() as ViewResult)?.Model;

			Assert.Equal(expectedViewName, actualViewName);
			Assert.NotNull(errorViewModel);
			Assert.IsType<NewGameModel>(errorViewModel);
		}


		[Fact]
		public void CreateGamePostCorrectModelReturnedOk()
		{
			var validGameModel = new NewGameModel
			{
				Name = "Test Game",
				Description = "Valid description for a game",
				Genre = GenreEnum.Family,
				Type = GameTypeEnum.Arcade
			};
			var expectedGame = validGameModel.getGame();

			var result = _sut.CreateGame(validGameModel) as IActionResult;

			Assert.NotNull(result);
			Assert.IsType<OkResult>(result);
			_gameRepository.Received(1).addGame(Arg.Is<Game>(g => g.Name == "Test Game" && g.Description == "Valid description for a game"));
		}

		[Fact]
		public void CreateGamePostFalseModelReturnedBadRequest()
		{
			var invalidGameModel = new NewGameModel();
			_sut.ModelState.AddModelError("PropertyName", "Error Message");

			var result = _sut.CreateGame(invalidGameModel);

			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			_gameRepository.DidNotReceive().addGame(Arg.Any<Game>());
			Assert.False((bool)badRequestResult.Value?.GetType().GetProperty("success")?.GetValue(badRequestResult.Value)!);
		}


		[Fact]
		public void GameDetailsViewReturnedWithValidGame()
		{
			const string expectedViewName = "GameDetails";
			Game game = new Game
			{
				Id = 3,
				Name = "Test Game",
			};
			_gameRepository.GetGameById(3).Returns(game);

			var result = _sut.GameDetails(3) as ViewResult;

			Assert.Equal(expectedViewName, result?.ViewName);
			Assert.NotNull(result?.Model);
			Assert.IsType<Game>(result?.Model);
			Assert.Equal(game, result?.Model);
		}


		[Fact]
		public void IndexRedirectWithInvalidGame()
		{
			const string expectedViewName = "Index";
			const string expectedControllerName = "Home";

			var result = _sut.GameDetails(4) as RedirectToActionResult;

			Assert.Equal(expectedViewName, result?.ActionName);
			Assert.Equal(expectedControllerName, result?.ControllerName);
		}

	}
}