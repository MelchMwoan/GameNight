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
	public class GameEFRepository : IGameRepository
	{
		private GameNightDbContext _dbContext;

		public GameEFRepository(GameNightDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Game> getGames()
		{
			return _dbContext.Games.ToList();
		}

		public Game? GetGameById(int id)
		{
			return _dbContext.Games.FirstOrDefault(x => x.Id == id);
		}

		public void addGame(Game game)
		{
			_dbContext.Games.Add(game);
			_dbContext.SaveChanges();
		}

		public void removeGame(Game game)
		{
			_dbContext.Games.Remove(game);
			_dbContext.SaveChanges();
		}
	}
}
