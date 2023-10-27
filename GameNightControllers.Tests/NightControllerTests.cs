using System.Numerics;
using System.Security.Claims;
using Domain;
using DomainServices;
using GameNight2.Controllers;
using GameNight2.Models;
using Infrastructure.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace GameNightControllers.Tests
{
	public class NightControllerTests
	{
		private NightController _sut;
		private ILogger<NightController> _logger = Substitute.For<ILogger<NightController>>();
		private INightRepository _nightRepository = Substitute.For<INightRepository>();
		private UserManager<GameNight2User> _userManager = Substitute.For<UserManager<GameNight2User>>(Substitute.For<IUserStore<GameNight2User>>(), Substitute.For<IOptions<IdentityOptions>>(), Substitute.For<IPasswordHasher<GameNight2User>>(), Substitute.For<IEnumerable<IUserValidator<GameNight2User>>>(), Substitute.For<IEnumerable<IPasswordValidator<GameNight2User>>>(), Substitute.For<ILookupNormalizer>(), Substitute.For<IdentityErrorDescriber>(), Substitute.For<IServiceProvider>(), Substitute.For<ILogger<UserManager<GameNight2User>>>());
		private IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
		private IGameRepository _gameRepository = Substitute.For<IGameRepository>();
		private ISnackRepository _snackRepository = Substitute.For<ISnackRepository>();

		public NightControllerTests()
		{
			_sut = new NightController(_logger, _nightRepository, _userManager, _accountRepository, _gameRepository, _snackRepository);
		}

		[Fact]
		public void GetViewOfNights()
		{
			const string expectedViewName = "Nights";
			List<Night> listNights = new List<Night>
			{
				new Night { Id = 1, },
				new Night { Id = 2, },
				new Night { Id = 3, },
			};
			_nightRepository.getNights().Returns(listNights);

			var result = _sut.Nights() as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal(expectedViewName, result.ViewName);
			Assert.Equal(listNights, result.Model);
		}

		[Fact]
		public void NightDetailsRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};

			var result = _sut.NightDetails(1) as RedirectResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectResult>(result);
			Assert.Contains("/Identity/Account/Login", result.Url);
			Assert.Contains("ReturnUrl=", result.Url);
		}

		[Fact]
		public void NightDetailsRedirectIndexWithInvalidNight()
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

			var result = _sut.NightDetails(1) as RedirectToActionResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", result.ActionName);
			Assert.Equal("Home", result.ControllerName);
		}

		[Fact]
		public void NightDetailsViewReturnedWithValidNight()
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
			NightPersonJoinResult nightPersonJoinResult = new NightPersonJoinResult();
			_nightRepository.getNightById(1).Returns(nightPersonJoinResult);

			var result = _sut.NightDetails(1) as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("NightDetails", result?.ViewName);
			Assert.Equal(nightPersonJoinResult, result?.Model);

		}

		[Fact]
		public void CreateNightRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};

			var result = _sut.CreateNight() as RedirectResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectResult>(result);
			Assert.Contains("/Identity/Account/Login", result.Url);
			Assert.Contains("ReturnUrl=", result.Url);
		}

		[Fact]
		public void CreateNightReturnsView()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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

			var result = _sut.CreateNight() as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("CreateNight", result?.ViewName);
			Assert.Null(result?.Model);
		}

		[Fact]
		public void EditNightRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};

			var result = _sut.EditNight(1) as RedirectResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectResult>(result);
			Assert.Contains("/Identity/Account/Login", result.Url);
			Assert.Contains("ReturnUrl=", result.Url);
		}

		[Fact]
		public void EditNightReturnsViewAndModel()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			NightPersonJoinResult nightPersonJoinResult = new NightPersonJoinResult
			{
				Night = new Night
				{
					Id = 1,
					DateTime = DateTime.Now,
					MaxPlayers = 5,
					Title = "Test Night",
					Description = "Description for test night"
				}
			};
			_nightRepository.getNightById(1).Returns(nightPersonJoinResult);
			var result = _sut.EditNight(1) as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("EditNight", result?.ViewName);
			Assert.IsType<EditNightModel>(result?.Model);
		}

		[Fact]
		public void UpdateNightRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};
			EditNightModel editNightModel = new EditNightModel();

			var result = _sut.UpdateNight(editNightModel) as RedirectToPageResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectToPageResult>(result);
			Assert.Equal("/Account/Login", result.PageName);
			Assert.Equal("Identity", result?.RouteValues?["area"]);
		}

		[Fact]
		public void UpdateNightReturnViewOnInvalidModel()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			EditNightModel editNightModel = new EditNightModel();
			_sut.ModelState.AddModelError("PropertyName", "Error Message");

			var result = _sut.UpdateNight(editNightModel) as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("EditNight", result?.ViewName);
			Assert.Equal(editNightModel, result?.Model);
		}
		[Fact]
		public void UpdateNightThrowErrorOnInvalidNight()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			EditNightModel editNightModel = new EditNightModel
			{
				Id = 1,
			};
			_accountRepository.getAccount("TestUser").Returns(new Person());
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)null);
			_sut.ModelState.Clear();

			var exception = Assert.Throws<Exception>(() => _sut.UpdateNight(editNightModel));

			Assert.NotNull(exception);
			Assert.Equal("Night doesn't exist", exception?.Message);
		}
		[Fact]
		public void UpdateNightThrowErrorOnNightWithPersons()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			EditNightModel editNightModel = new EditNightModel
			{
				Id = 1,
			};
			Night night = new Night
			{
				Id = 1,
				Players = new List<Person>
				{
					new Person()
				}
			};
			_accountRepository.getAccount("TestUser").Returns(new Person());
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)new NightPersonJoinResult{Night = night, Person = new Person()});
			_sut.ModelState.Clear();

			var exception = Assert.Throws<Exception>(() => _sut.UpdateNight(editNightModel));

			Assert.NotNull(exception);
			Assert.Equal("Can't edit the night while there are attendees", exception?.Message);
		}
		[Fact]
		public void UpdateNightSuccess()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			EditNightModel editNightModel = new EditNightModel
			{
				Id = 1,
				Title = "New Title for Test"
			};
			Night night = new Night
			{
				Id = 1,
				Title = "Old Title for Test"
			};
			_accountRepository.getAccount("TestUser").Returns(new Person());
			_gameRepository.getGames().Returns(new List<Game>());
			_snackRepository.getSnacks().Returns(new List<Snack>());
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)new NightPersonJoinResult { Night = night, Person = new Person() });
			_sut.ModelState.Clear();

			var result = _sut.UpdateNight(editNightModel) as RedirectToActionResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("NightDetails", result.ActionName);
			Assert.Equal("Night", result.ControllerName);
			Assert.Equal(1, result.RouteValues?["id"]);
		}
		[Fact]
		public void RemoveNightThrowErrorOnInvalidNight()
		{
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)null);

			var exception = Assert.Throws<Exception>(() => _sut.RemoveNight(1));

			Assert.NotNull(exception);
			Assert.Equal("Night doesn't exist", exception?.Message);
		}
		[Fact]
		public void RemoveNightThrowErrorOnNightWithPersons()
		{
			Night night = new Night
			{
				Id = 1,
				Players = new List<Person>
				{
					new Person()
				}
			};
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)new NightPersonJoinResult { Night = night, Person = new Person() });

			var exception = Assert.Throws<Exception>(() => _sut.RemoveNight(1));

			Assert.NotNull(exception);
			Assert.Equal("Can't remove the night while there are attendees", exception?.Message);
		}
		[Fact]
		public void RemoveNightSuccess()
		{
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)new NightPersonJoinResult { Night = new Night(), Person = new Person() });

			var result = _sut.RemoveNight(1) as RedirectToActionResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Nights", result.ActionName);
			Assert.Equal("Night", result.ControllerName);
		}


		[Fact]
		public void CreateNightReturnViewOnInvalidModel()
		{
			NewNightModel newNightModel = new NewNightModel();
			_sut.ModelState.AddModelError("PropertyName", "Error Message");

			var result = _sut.CreateNight(newNightModel) as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("CreateNight", result?.ViewName);
			Assert.Equal(newNightModel, result?.Model);
		}

		[Fact]
		public void CreateNightReturnViewSuccess()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			NewNightModel newNightModel = new NewNightModel
			{
				Title = "Test Title",
				Description = "Test description",
				DateTime = DateTime.Now,
				MaxPlayers = 3,
			};
			_accountRepository.getAccount("TestUser").Returns(new Person { Id = 1 });
			_gameRepository.getGames().Returns(new List<Game>());
			_snackRepository.getSnacks().Returns(new List<Snack>());

			var result = _sut.CreateNight(newNightModel) as RedirectToActionResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("NightDetails", result?.ActionName);
		}

		[Fact]
		public void FilterNightsReturnViewOnInvalidFilter()
		{
			NightsFilterModel nightsFilterModel = new NightsFilterModel();
			_sut.ModelState.AddModelError("PropertyName", "Error Message");
			List<Night> listNights = new List<Night>();
			_nightRepository.getNights().Returns(listNights);

			var result = _sut.FilterNights(nightsFilterModel) as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("Nights", result?.ViewName);
			Assert.Equal(listNights, result?.Model);
		}
		[Fact]
		public void FilterNightsReturnViewOnValidFilter()
		{
			NightsFilterModel nightsFilterModel = new NightsFilterModel { Name = "test" };
			List<Night> listNights = new List<Night>();
			_nightRepository.filterNights(Arg.Any<NightFilter>()).Returns(listNights);

			var result = _sut.FilterNights(nightsFilterModel) as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("Nights", result?.ViewName);
			Assert.Equal(listNights, result?.Model);
		}

		[Fact]
		public void GetHostedNightsRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};

			var result = _sut.GetHostedNights() as RedirectResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectResult>(result);
			Assert.Contains("/Identity/Account/Login", result.Url);
			Assert.Contains("ReturnUrl=", result.Url);
		}

		[Fact]
		public void GetHostedNightsSuccess()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			List<Night> returnNights = new List<Night>();;
			_nightRepository.getHostedNights(1).Returns(returnNights);
			_accountRepository.getAccount("TestUser").Returns(new Person { Id = 1 });

			var result = _sut.GetHostedNights() as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("GetHostedNights", result?.ViewName);
			Assert.Equal(returnNights, result?.Model);
		}

		[Fact]
		public void GetJoinedNightsRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};

			var result = _sut.GetJoinedNights() as RedirectResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectResult>(result);
			Assert.Contains("/Identity/Account/Login", result.Url);
			Assert.Contains("ReturnUrl=", result.Url);
		}

		[Fact]
		public void GetJoinedNightsSuccess()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			List<Night> returnNights = new List<Night>(); ;
			_nightRepository.getJoinedNights(1).Returns(returnNights);
			_accountRepository.getAccount("TestUser").Returns(new Person { Id = 1 });

			var result = _sut.GetJoinedNights() as ViewResult;

			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
			Assert.Equal("GetJoinedNights", result?.ViewName);
			Assert.Equal(returnNights, result?.Model);
		}

		[Fact]
		public void JoinNightRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};

			var result = _sut.JoinNight(1) as RedirectResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectResult>(result);
			Assert.Contains("/Identity/Account/Login", result.Url);
			Assert.Contains("ReturnUrl=", result.Url);
		}
		[Fact]
		public void JoinNightThrowErrorOnInvalidNight()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)null);

			var exception = Assert.Throws<Exception>(() => _sut.JoinNight(1));

			Assert.NotNull(exception);
			Assert.Equal("Night doesn't exist", exception?.Message);
		}
		[Fact]
		public void JoinNightThrowErrorOnBusyNight()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			_accountRepository.getAccount("TestUser").Returns(new Person{ Id = 1});
			_nightRepository.getJoinedNights(1).Returns(new List<Night> { new Night { DateTime = DateTime.Now } });
			_nightRepository.getNightById(1).Returns(new NightPersonJoinResult{Night = new Night{Id = 1, DateTime = DateTime.Now}, Person = new Person()});

			var exception = Assert.Throws<Exception>(() => _sut.JoinNight(1));

			Assert.NotNull(exception);
			Assert.Equal("Can't join 2 nights on the same day", exception?.Message);
		}
		[Fact]
		public void JoinNightSuccess()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			_accountRepository.getAccount("TestUser").Returns(new Person{Id = 1});
			_nightRepository.getJoinedNights(1).Returns(new List<Night>());
			_nightRepository.getNightById(1).Returns(new NightPersonJoinResult { Night = new Night { Id = 1 }, Person = new Person() });

			var result = _sut.JoinNight(1) as RedirectToActionResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("NightDetails", result.ActionName);
			Assert.Equal("Night", result.ControllerName);
			Assert.Equal(1, result?.RouteValues?["id"]);
		}

		[Fact]
		public void LeaveNightRedirectToLogin()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity())
				}
			};

			var result = _sut.LeaveNight(1) as RedirectResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectResult>(result);
			Assert.Contains("/Identity/Account/Login", result.Url);
			Assert.Contains("ReturnUrl=", result.Url);
		}
		[Fact]
		public void LeaveNightThrowErrorOnInvalidNight()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			_nightRepository.getNightById(1).Returns((NightPersonJoinResult?)null);

			var exception = Assert.Throws<Exception>(() => _sut.JoinNight(1));

			Assert.NotNull(exception);
			Assert.Equal("Night doesn't exist", exception?.Message);
		}
		[Fact]
		public void LeaveNightSuccess()
		{
			var urlHelper = Substitute.For<IUrlHelper>();
			_sut.Url = urlHelper;
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
			_nightRepository.getNightById(1).Returns(new NightPersonJoinResult { Night = new Night{Id=1}, Person = new Person() });

			var result = _sut.LeaveNight(1) as RedirectToActionResult;

			Assert.NotNull(result);
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("NightDetails", result.ActionName);
			Assert.Equal("Night", result.ControllerName);
			Assert.Equal(1, result?.RouteValues?["id"]);
		}

	}
}