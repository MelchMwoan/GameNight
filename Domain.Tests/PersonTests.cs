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

		[Fact]
		public void SetBirthDateFarFuture()
		{
			Person testPersonCopy = testPerson;
			DateTime birthdate = DateTime.Now.AddYears(5);

			var exception = Record.Exception(() => testPersonCopy.SetBirthdate(birthdate));

			Assert.Equal("Your birthdate can't be in the future", exception?.Message);
		}
		[Fact]
		public void SetBirthDateFutureClose()
		{
			Person testPersonCopy = testPerson;
			DateTime birthdate = DateTime.Now.AddSeconds(5);

			var exception = Record.Exception(() => testPersonCopy.SetBirthdate(birthdate));

			Assert.Equal("Your birthdate can't be in the future", exception?.Message);
		}
		[Fact]
		public void SetBirthDateLessThan16()
		{
			Person testPersonCopy = testPerson;
			DateTime birthdate = DateTime.Now.AddYears(-12);

			var exception = Record.Exception(() => testPersonCopy.SetBirthdate(birthdate));

			Assert.Equal("You need to be at least 16 years to register", exception?.Message);
		}
		[Fact]
		public void SetBirthDateLessThan16Close()
		{
			Person testPersonCopy = testPerson;
			DateTime birthdate = DateTime.Now.AddSeconds(5).AddYears(-16);

			var exception = Record.Exception(() => testPersonCopy.SetBirthdate(birthdate));

			Assert.Equal("You need to be at least 16 years to register", exception?.Message);
		}
		[Fact]
		public void SetBirthDateNow()
		{
			Person testPersonCopy = testPerson;
			DateTime birthdate = DateTime.Now;

			var exception = Record.Exception(() => testPersonCopy.SetBirthdate(birthdate));

			Assert.Equal("You need to be at least 16 years to register", exception?.Message);
		}
		[Fact]
		public void SetBirthDateExact16()
		{
			Person testPersonCopy = testPerson;
			DateTime birthdate = DateTime.Now.AddYears(-16);

			var exception = Record.Exception(() => testPersonCopy.SetBirthdate(birthdate));

			Assert.Null(exception);
			Assert.Equal(testPersonCopy.BirthDate, birthdate);
		}
		[Fact]
		public void SetBirthDateFar16()
		{
			Person testPersonCopy = testPerson;
			DateTime birthdate = DateTime.Now.AddYears(-18);

			var exception = Record.Exception(() => testPersonCopy.SetBirthdate(birthdate));

			Assert.Null(exception);
			Assert.Equal(testPersonCopy.BirthDate, birthdate);
		}
	}
}