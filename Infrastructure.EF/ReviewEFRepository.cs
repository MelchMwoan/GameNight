using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using SQLData;

namespace Infrastructure.EF
{
	public class ReviewEFRepository : IReviewRepository
	{
		private GameNightDbContext _dbContext;

		public ReviewEFRepository(GameNightDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Review> GetReviews()
		{
			return _dbContext.Reviews.ToList();
		}

		public Review? GetReviewById(int id)
		{
			return _dbContext.Reviews.FirstOrDefault(r => r.Id == id);
		}

		public List<Review> GetReviewByNight(int nightId)
		{
			return _dbContext.Reviews.Where(r => r.nightId == nightId).Include(r => r.Writer).Include(r=>r.Organisator).ToList();
		}

		public List<Review> GetReviewByOrganisator(int organisatorId)
		{
			return _dbContext.Reviews.Where(r => r.night.Organisator.Id == organisatorId).ToList();
		}

		public void AddReview(Review review)
		{
			_dbContext.Reviews.Add(review);
			_dbContext.SaveChanges();
		}

		public void RemoveReview(Review review)
		{
			_dbContext.Reviews.Remove(review);
			_dbContext.SaveChanges();
		}
	}
}
