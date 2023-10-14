using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure.EF;

namespace DomainServices
{
	public interface INightRepository
	{
		List<Night> getNights();
		List<Night> filterNights(NightFilter filter);
		NightPersonJoinResult? getNightById(int id);
		List<Night> getHostedNights(int userId);
		List<Night> getJoinedNights(int userId);
		void addNight(Night night);
		void removeNight(Night night);
		void updateNight(Night night);

		void joinNight(int nightId, Person person);
		void leaveNight(int nightId, Person person);
	}
}
