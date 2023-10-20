using Xunit;

namespace Domain.Tests
{
	public class NightTests
	{
		[Fact]
		public void AddPlayerWhereEmailIsSameAsOrganisatorEmail()
		{
			//Arrange
			Person organisator = new Person()
			{
				Email = "organisator@night.test.nl"
			};

			Night night = new Night()
			{
				Organisator = organisator
			};

			Person player = new Person()
			{
				Email = "organisator@night.test.nl"
			};

			//Act
			Action act = () => night.AddPlayer(player);

			//Assert
			Exception exception = Assert.Throws<Exception>(act);
			Assert.Equal("Can't join your own night", exception.Message);
		}
	}
}