using System.Numerics;
using Xunit;
using Xunit.Sdk;

namespace Domain.Tests
{
	public class ReviewTests
	{
		private Review testReview = new Review
		{
			Rating = 3.5,
			Description = "This is a test review for the ReviewTests"
		};

		[Fact]
		public void SetWriterSuccess()
		{
			Review copyTestReview = testReview;
			Person writer = new Person();

			var exception = Record.Exception(() => copyTestReview.setWriter(writer));

			Assert.Null(exception);
			Assert.Equal(writer, copyTestReview.Writer);
			Assert.Equal(writer.Id, copyTestReview.writerId);
		}

		[Fact]
		public void SetWriterAgain()
		{
			Review copyTestReview = testReview;
			Person writer = new Person();
			copyTestReview.setWriter(writer);

			var exception = Record.Exception(() => copyTestReview.setWriter(writer));

			Assert.Null(exception);
			Assert.Equal(writer, copyTestReview.Writer);
			Assert.Equal(writer.Id, copyTestReview.writerId);
		}

		[Fact]
		public void SetDifferentWriter()
		{
			Review copyTestReview = testReview;
			Person writer = new Person();
			Person newWriter = new Person();
			copyTestReview.setWriter(writer);

			var exception = Record.Exception(() => copyTestReview.setWriter(newWriter));

			Assert.Null(exception);
			Assert.Equal(newWriter, copyTestReview.Writer);
			Assert.Equal(newWriter.Id, copyTestReview.writerId);
		}

		[Fact]
		public void SetWriterNull()
		{
			Review copyTestReview = testReview;
			Person writer = null;

			var exception = Record.Exception(() => copyTestReview.setWriter(writer));

			Assert.IsType<NullReferenceException>(exception);
		}

		[Fact]
		public void SetOrganisatorSuccess()
		{
			Review copyTestReview = testReview;
			Person organisator = new Person();

			var exception = Record.Exception(() => copyTestReview.setOrganisator(organisator));

			Assert.Null(exception);
			Assert.Equal(organisator, copyTestReview.Organisator);
			Assert.Equal(organisator.Id, copyTestReview.organisatorId);
		}

		[Fact]
		public void SetOrganisatorAgain()
		{
			Review copyTestReview = testReview;
			Person organisator = new Person();
			copyTestReview.setOrganisator(organisator);

			var exception = Record.Exception(() => copyTestReview.setOrganisator(organisator));

			Assert.Null(exception);
			Assert.Equal(organisator, copyTestReview.Organisator);
			Assert.Equal(organisator.Id, copyTestReview.organisatorId);
		}

		[Fact]
		public void SetDifferentOrganisator()
		{
			Review copyTestReview = testReview;
			Person organisator = new Person();
			Person newOrganisator = new Person();
			copyTestReview.setOrganisator(organisator);

			var exception = Record.Exception(() => copyTestReview.setOrganisator(newOrganisator));

			Assert.Null(exception);
			Assert.Equal(newOrganisator, copyTestReview.Organisator);
			Assert.Equal(newOrganisator.Id, copyTestReview.organisatorId);
		}

		[Fact]
		public void SetOrganisatorNull()
		{
			Review copyTestReview = testReview;
			Person organisator = null;

			var exception = Record.Exception(() => copyTestReview.setOrganisator(organisator));

			Assert.IsType<NullReferenceException>(exception);
		}

		[Fact]
		public void SetNightSuccess()
		{
			Review copyTestReview = testReview;
			Night night = new Night();

			var exception = Record.Exception(() => copyTestReview.setNight(night));

			Assert.Null(exception);
			Assert.Equal(night, copyTestReview.night);
			Assert.Equal(night.Id, copyTestReview.nightId);
		}

		[Fact]
		public void SetNightAgain()
		{
			Review copyTestReview = testReview;
			Night night = new Night();
			copyTestReview.setNight(night);

			var exception = Record.Exception(() => copyTestReview.setNight(night));

			Assert.Null(exception);
			Assert.Equal(night, copyTestReview.night);
			Assert.Equal(night.Id, copyTestReview.nightId);
		}

		[Fact]
		public void SetDifferentNight()
		{
			Review copyTestReview = testReview;
			Night night = new Night();
			Night newNight = new Night();
			copyTestReview.setNight(night);

			var exception = Record.Exception(() => copyTestReview.setNight(newNight));

			Assert.Null(exception);
			Assert.Equal(newNight, copyTestReview.night);
			Assert.Equal(newNight.Id, copyTestReview.nightId);
		}

		[Fact]
		public void SetNightNull()
		{
			Review copyTestReview = testReview;
			Night night = null;

			var exception = Record.Exception(() => copyTestReview.setNight(night));

			Assert.IsType<NullReferenceException>(exception);
		}
	}
}