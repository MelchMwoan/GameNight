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
		void addSnack(Snack snack);
		void removeSnack(Snack snack);
	}
}
