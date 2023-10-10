using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices
{
	public interface IAccountRepository
	{
		void createAccount(Person person);
		void deleteAccount(int personId);
		void updateAccount(int personId, Person person);
		Person getAccount(string personEmail);
	}
}
