using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
	public class Night
	{
		public string Title { get; set; } = "Game Night";
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public int MaxPlayers { get; set; }
		public List<Game> Games { get; set; }
		public List<Person> Players { get; set; } = new List<Person>();
		public int PersonId { get; set; }
		public Person Organisator { get; set; }
		public List<Review> Reviews { get; set; }
		public List<Snack> Snacks { get; set; }
		public string ThumbnailUrl { get; set; } = "https://s.yimg.com/ny/api/res/1.2/GjwW45jyFilXfDhM3Pl5rQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTEyMDA7aD02NzU-/https://media.zenfs.com/en/gobankingrates_644/4464a542bd2bf3c1300d1ae8de26441b";

		public void AddPlayer(Person player) { Players.Add(player); }

		public void AddGame(Game game) { Games.Add(game); }

		public void AddReview(Review review) { Reviews.Add(review); }

		public void AddSnack(Snack snack) { Snacks.Add(snack); }
	}
}
