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
	public class HomeControllerTests
	{
		private HomeController _sut;
		private ILogger<HomeController> _logger = Substitute.For<ILogger<HomeController>>();

		public HomeControllerTests()
		{
			_sut = new HomeController(_logger);
		}

		[Fact]
		public void IndexViewReturned()
		{
			const string expectedViewName = "Index";

			var actualViewName = (_sut.Index() as ViewResult)?.ViewName;

			Assert.Equal(expectedViewName, actualViewName);
		}

		[Fact]
		public void PrivacyviewReturned()
		{
			const string expectedViewName = "Privacy";

			var actualViewName = (_sut.Privacy() as ViewResult)?.ViewName;

			Assert.Equal(expectedViewName, actualViewName);
		}

		[Fact]
		public void AccountViewReturned()
		{
			const string expectedViewName = "Account";

			var actualViewName = (_sut.Account() as ViewResult)?.ViewName;

			Assert.Equal(expectedViewName, actualViewName);
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