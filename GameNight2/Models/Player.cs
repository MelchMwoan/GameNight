namespace GameNight2.Models
{
	public class Player : Person
	{
		public List<Game> Games { get; set; }


		public void addGame(Game game) { Games.Add(game); }
	}
}
