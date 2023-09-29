namespace GameNight2.Models
{
	public class Night
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public int MaxPlayers { get; set; }
		public List<Game> Games { get; set; }
		public List<Player> Players { get; set; }
		public Organisator Organisator { get; set; }
		public List<Review> Reviews { get; set; }
		public List<Snack> Snacks { get; set; }

		public void AddPlayer(Player player) { Players.Add(player); }

		public void AddGame(Game game) { Games.Add(game); }

		public void AddReview(Review review) { Reviews.Add(review); }

		public void AddSnack(Snack snack) { Snacks.Add(snack); }
	}
}
