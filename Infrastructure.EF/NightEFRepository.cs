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
	public class NightEFRepository : INightRepository
	{
		private GameNightDbContext _dbContext;

		public NightEFRepository(GameNightDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Night> getNights()
		{
			return _dbContext.Nights.ToList();
		}

		public NightPersonJoinResult? getNightById(int id)
		{
			return _dbContext.Nights
				.Join(
					_dbContext.Persons,
					night => night.PersonId,
					person => person.Id,
					(night, person) => new NightPersonJoinResult
					{
						Night = night,
						Person = person
					})
				.FirstOrDefault(x => x.Night.Id == id);
		}

		public List<Night> getHostedNights(int userId)
		{
			return _dbContext.Nights.Where(night => night.PersonId == userId).ToList();
		}

		public List<Night> getJoinedNights(int userId)
		{
			throw new NotImplementedException();
		}

		public void addNight(Night night)
		{
			_dbContext.Nights.Add(night);
			_dbContext.SaveChanges();
		}

		public void removeNight(Night night)
		{
			throw new NotImplementedException();
		}

		public void joinNight(int nightId, Person person)
		{
			Night night = _dbContext.Nights.SingleOrDefault(x => x.Id == nightId);
			night.AddPlayer(person);
			Console.WriteLine(night == null);
			Console.WriteLine(person == null);
			_dbContext.SaveChanges();
		}

		public void leaveNight(int nightId, Person person)
		{
			throw new NotImplementedException();
		}
	}
}
