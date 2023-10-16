using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Policy;
using Domain;

namespace GameNight2.Models
{
	public class NewGameModel
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Version { get; set; } = "Latest Version";
		public bool Is18Plus { get; set; }
		public GenreEnum Genre { get; set; }
		public GameTypeEnum Type { get; set; }
		public string? ImageUrl { get; set; } = "https://t4.ftcdn.net/jpg/04/73/25/49/360_F_473254957_bxG9yf4ly7OBO5I0O5KABlN930GwaMQz.jpg";

		public Game getGame()
		{
			return new Game()
			{
				Name = this.Name,
				Description = this.Description,
				Version = this.Version,
				Is18Plus = this.Is18Plus,
				Genre = this.Genre,
				Type = this.Type,
				ImageUrl = this.ImageUrl
			};
		}
	}
}
