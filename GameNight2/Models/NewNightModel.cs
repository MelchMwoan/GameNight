using System.ComponentModel.DataAnnotations;

namespace GameNight2.Models
{
	public class NewNightModel
	{
		public string Title { get; set; }
		public DateTime DateTime { get; set; }
		[Range(2, 100, ErrorMessage = "MaxPlayers should be at least 2")]
		public int MaxPlayers { get; set; }
		[Url]
		public string ThumbnailUrl { get; set; }
	}
}
