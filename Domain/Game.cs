using Domain;

namespace Domain
{
	public class Game
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Version { get; set; }
		public bool Is18Plus { get; set; }
		public GenreEnum Genre { get; set; }
		public GameTypeEnum Type { get; set; }
		public string ImageUrl { get; set; }
		public List<Night> Nights { get; set; }= new List<Night>();
	}
}
