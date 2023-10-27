using System.Numerics;
using Xunit;

namespace Domain.Tests
{
	public class NightTests
	{

		private Night testNight = new Night
		{
			Title = "TestNight",
			Description = "A night specifically created to test the Night domain",
			DateTime = DateTime.Now,
			Organisator = new Person
			{
				Address = new Address
				{
					City = "Breda",
					Street = "Lovensdijkstraat",
					HouseNumber = 61
				},
				BirthDate = DateTime.Parse("01/01/2001"),
				Email = "organisator@night.test.nl",
				Gender = GenderEnum.Other,
			},
			AdultOnly = false,
			MaxPlayers = 5
		};

		[Fact]
		public void AddPlayerWhereEmailIsSameAsOrganisatorEmail()
		{
			Night testNightCopy = testNight;
			Person player = new Person()
			{
				Email = "organisator@night.test.nl"
			};

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player));

			Assert.Equal("Can't join your own night", exception?.Message);
		}

		[Fact]
		public void AddPlayerSuccess()
		{
			Night testNightCopy = testNight;
			Person player = new Person();

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player));

			Assert.Null(exception);
			Assert.Contains(player, testNightCopy.Players);
		}
		[Fact]
		public void AddPlayerDouble()
		{
			Night testNightCopy = testNight;
			Person player = new Person();
			testNightCopy.AddPlayer(player);

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player));

			Assert.Null(exception);
			Assert.Contains(player, testNightCopy.Players);
		}

		[Fact]
		public void AddPlayerNightIsFull()
		{
			Night testNightCopy = testNight;
			testNightCopy.MaxPlayers = 3;
			Person player1 = new Person();
			Person player2 = new Person();
			Person player3 = new Person();
			Person player4 = new Person();
			testNightCopy.AddPlayer(player1);
			testNightCopy.AddPlayer(player2);
			testNightCopy.AddPlayer(player3);

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player4));

			Assert.Equal("The Night is already full", exception?.Message);
		}

		[Fact]
		public void AddPlayerTooYoungClose()
		{
			Night testNightCopy = testNight;
			testNightCopy.AdultOnly = true;
			Person player = new Person
			{
				BirthDate = DateTime.Now.AddYears(-18).AddSeconds(1)
			};

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player));

			Assert.Equal("The Night is for adults only", exception?.Message);
		}

		[Fact]
		public void AddPlayerTooYoung()
		{
			Night testNightCopy = testNight;
			testNightCopy.AdultOnly = true;
			Person player = new Person
			{
				BirthDate = DateTime.Now.AddYears(-12)
			};

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player));

			Assert.Equal("The Night is for adults only", exception?.Message);
		}

		[Fact]
		public void AddPlayer18Exact()
		{
			Night testNightCopy = testNight;
			testNightCopy.AdultOnly = true;
			Person player = new Person
			{
				BirthDate = DateTime.Now.AddYears(-18)
			};

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player));

			Assert.Null(exception);
		}

		[Fact]
		public void AddPlayerOld()
		{
			Night testNightCopy = testNight;
			testNightCopy.AdultOnly = true;
			Person player = new Person
			{
				BirthDate = DateTime.Now.AddYears(-25)
			};

			var exception = Record.Exception(() => testNightCopy.AddPlayer(player));

			Assert.Null(exception);
		}


		[Fact]
		public void RemovePlayerSuccess()
		{
			Night testNightCopy = testNight;
			Person player = new Person();

			testNightCopy.AddPlayer(player);
			var exception = Record.Exception(() => testNightCopy.RemovePlayer(player));

			Assert.Null(exception);
			Assert.DoesNotContain(player, testNightCopy.Players);
		}

		[Fact]
		public void RemovePlayerNotExistent()
		{
			Night testNightCopy = testNight;
			Person player = new Person();

			var exception = Record.Exception(() => testNightCopy.RemovePlayer(player));

			Assert.Null(exception);
			Assert.DoesNotContain(player, testNightCopy.Players);
		}


		[Fact]
		public void AddGameSuccess()
		{
			Night testNightCopy = testNight;
			Game game = new Game();

			var exception = Record.Exception(() => testNightCopy.AddGame(game));

			Assert.Null(exception);
			Assert.Contains(game, testNightCopy.Games);
		}
		[Fact]
		public void AddGameDouble()
		{
			Night testNightCopy = testNight;
			Game game = new Game();
			testNightCopy.AddGame(game);

			var exception = Record.Exception(() => testNightCopy.AddGame(game));

			Assert.Null(exception);
			Assert.Contains(game, testNightCopy.Games);
		}

		[Fact]
		public void AddGameAdult()
		{
			Night testNightCopy = testNight;
			Game game = new Game
			{
				Is18Plus = true
			};

			var exception = Record.Exception(() => testNightCopy.AddGame(game));

			Assert.Null(exception);
			Assert.True(testNightCopy.AdultOnly);
		}

		[Fact]
		public void AddReviewSuccess()
		{
			Night testNightCopy = testNight;
			Review review = new Review();

			var exception = Record.Exception(() => testNightCopy.AddReview(review));

			Assert.Null(exception);
			Assert.Contains(review, testNightCopy.Reviews);
		}
		[Fact]
		public void AddReviewDouble()
		{
			Night testNightCopy = testNight;
			Review review = new Review();
			testNightCopy.AddReview(review);

			var exception = Record.Exception(() => testNightCopy.AddReview(review));

			Assert.Null(exception);
			Assert.Contains(review, testNightCopy.Reviews);
		}

		[Fact]
		public void AddSnackSuccess()
		{
			Night testNightCopy = testNight;
			Snack snack = new Snack();

			var exception = Record.Exception(() => testNightCopy.AddSnack(snack));

			Assert.Null(exception);
			Assert.Contains(snack, testNightCopy.Snacks);
		}
		[Fact]
		public void AddSnackDouble()
		{
			Night testNightCopy = testNight;
			Snack snack = new Snack();
			testNightCopy.AddSnack(snack);

			var exception = Record.Exception(() => testNightCopy.AddSnack(snack));

			Assert.Null(exception);
			Assert.Contains(snack, testNightCopy.Snacks);
		}
	}
}