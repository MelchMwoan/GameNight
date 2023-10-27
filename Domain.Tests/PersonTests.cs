using Xunit;

namespace Domain.Tests
{
	public class PersonTests
	{
		private Person testPerson = new Person
		{
			Name = "TestPerson",
			RealName = "FirstName LastName",
			Email = "testPerson@person.test.nl",
			Gender = GenderEnum.Other,
			BirthDate = DateTime.Parse("01/01/2001"),
			Address = new Address
			{
				City = "Breda",
				HouseNumber = 1,
				Street = "Lovensdijkstraat"
			},
			Games = new List<Game>(),
			Nights = new List<Night>()
		};

		[Fact]
		public void AddGameSuccess()
		{
			Person testPersonCopy = testPerson;
			Game game = new Game();

			var exception = Record.Exception(() => testPersonCopy.addGame(game));

			Assert.Null(exception);
			Assert.Contains(game, testPersonCopy.Games);
		}


		[Fact]
		public void AddNightSuccess()
		{
			Person testPersonCopy = testPerson;
			Night night = new Night();

			var exception = Record.Exception(() => testPersonCopy.addNight(night));

			Assert.Null(exception);
			Assert.Contains(night, testPersonCopy.Nights);
		}
	}
}