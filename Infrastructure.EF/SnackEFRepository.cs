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
	public class SnackEFRepository : ISnackRepository
	{
		private GameNightDbContext _dbContext;

		public SnackEFRepository(GameNightDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Snack> getSnacks()
		{
			return _dbContext.Snacks.ToList();
		}

		public Snack? getSnackById(int id)
		{
			return _dbContext.Snacks.FirstOrDefault(x => x.Id == id);
		}

		public List<Snack> getSnackByNight(int nightId)
		{
			return _dbContext.Snacks.Where(x => x.night.Id == nightId).ToList();
		}

		public List<Snack> getSnackByPerson(int personId)
		{
			return _dbContext.Snacks.Where(x => x.personId == personId && x.nightId == null).ToList();
		}

		public void setSnackNight(int snackId, Night night)
		{
			Snack snack = getSnackById(snackId);
			snack.SetNight(night);
			_dbContext.SaveChanges();
		}

		public void addSnack(Snack snack)
		{
			_dbContext.Snacks.Add(snack);
			_dbContext.SaveChanges();
		}

		public void removeSnack(Snack snack)
		{
			_dbContext.Snacks.Remove(snack);
			_dbContext.SaveChanges();
		}
	}
}
