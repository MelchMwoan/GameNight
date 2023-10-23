using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure.EF;

namespace DomainServices
{
	public interface IReviewRepository
	{
		List<Review> GetReviews();
		Review? GetReviewById(int id);
		List<Review> GetReviewByNight(int nightId);
		List<Review> GetReviewByOrganisator(int organisatorId);
		void AddReview(Review review);
		void RemoveReview(Review review);
	}
}
