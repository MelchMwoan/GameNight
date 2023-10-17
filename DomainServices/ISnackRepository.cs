using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure.EF;

namespace DomainServices
{
	public interface ISnackRepository
	{
		List<Snack> getSnacks();
		Snack? getSnackById(int id);
		List<Snack> getSnackByNight(int nightId);
		List<Snack> getSnackByPerson(int personId);
		void setSnackNight(int snackId, Night night);
		void addSnack(Snack snack);
		void removeSnack(Snack snack);
	}
}
