using Xunit;

namespace Domain.Tests
{
	public class SnackTests
	{
		private Snack testSnack = new Snack
		{
			Name = "TestSnack",
		};

		[Fact]
		public void SetNightSuccess()
		{
			Snack copyTestSnack = testSnack;
			Night night = new Night();

			var exception = Record.Exception(() => copyTestSnack.SetNight(night));

			Assert.Null(exception);
			Assert.Equal(night, copyTestSnack.night);
			Assert.Equal(night.Id, copyTestSnack.nightId);
		}

		[Fact]
		public void SetNightAgain()
		{
			Snack copyTestSnack = testSnack;
			Night night = new Night();
			copyTestSnack.SetNight(night);

			var exception = Record.Exception(() => copyTestSnack.SetNight(night));

			Assert.Null(exception);
			Assert.Equal(night, copyTestSnack.night);
			Assert.Equal(night.Id, copyTestSnack.nightId);
		}

		[Fact]
		public void SetDifferentNight()
		{
			Snack copyTestSnack = testSnack;
			Night night = new Night();
			Night newNight = new Night();
			copyTestSnack.SetNight(night);

			var exception = Record.Exception(() => copyTestSnack.SetNight(newNight));

			Assert.Null(exception);
			Assert.Equal(newNight, copyTestSnack.night);
			Assert.Equal(newNight.Id, copyTestSnack.nightId);
		}

		[Fact]
		public void SetNightNull()
		{
			Snack copyTestSnack = testSnack;
			Night night = null;

			var exception = Record.Exception(() => copyTestSnack.SetNight(night));

			Assert.IsType<NullReferenceException>(exception);
		}
	}
}