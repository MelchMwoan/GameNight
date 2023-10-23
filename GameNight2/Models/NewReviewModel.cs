using System.ComponentModel.DataAnnotations;
using Domain;
using DomainServices;

namespace GameNight2.Models
{
	public class NewReviewModel
	{
		public NewReviewModel(int nightId)
		{
			this.nightId = nightId;
		}
		public NewReviewModel()
		{
			this.nightId = nightId;
		}
		public int nightId { get; set; }
		public double Rating { get; set; }
		public string Description { get; set; }

		public Review GetReview()
		{
			return new Review
			{
				nightId = this.nightId,
				Rating = this.Rating,
				Description = this.Description
			};
		}
	}
}
