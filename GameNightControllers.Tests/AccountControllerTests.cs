using Domain;
using DomainServices;
using GameNight2.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace GameNightControllers.Tests
{
	public class AccountControllerTests
	{
		private AccountController _sut;
		private ILogger<AccountController> _logger = Substitute.For<ILogger<AccountController>>();
		private IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();

		public AccountControllerTests()
		{
			_sut = new AccountController(_logger, _accountRepository);
		}

		[Fact]
		public void ErrorViewAndModelReturned()
		{
			const string expectedViewName = "Error";

			var actualViewName = (_sut.Error() as ViewResult)?.ViewName;
			var errorViewModel = (_sut.Error() as ViewResult)?.Model;

			Assert.Equal(expectedViewName, actualViewName);
			Assert.NotNull(errorViewModel);
			Assert.IsType<ErrorViewModel>(errorViewModel);
		}

	}
}