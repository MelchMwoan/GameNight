using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure.EF;

namespace DomainServices
{
	public interface IGameRepository
	{
		List<Game> getGames();
		Game? GetGameById(int id);
		void addGame(Game game);
		void removeGame(Game game);
	}
}
