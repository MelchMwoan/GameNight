using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices;
using SQLData;

namespace Infrastructure.EF
{
	public class AccountEFRepository : IAccountRepository
	{
		private GameNightDbContext _dbContext;

		public AccountEFRepository(GameNightDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void createAccount(Person person)
		{
			_dbContext.Persons.Add(person);
			_dbContext.SaveChanges();
		}

		public void deleteAccount(int personId)
		{
			throw new NotImplementedException();
		}

		public void updateAccount(int personId, Person person)
		{
			throw new NotImplementedException();
		}

		public Person getAccount(string personEmail)
		{
			return _dbContext.Persons.FirstOrDefault(x => x.Email == personEmail);
		}
	}
}
