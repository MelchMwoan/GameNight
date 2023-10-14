using System.ComponentModel.DataAnnotations;
using Domain;

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

		public Night getNight()
		{
			return new Night
			{
				Title = this.Title,
				DateTime = this.DateTime,
				MaxPlayers = this.MaxPlayers,
				ThumbnailUrl = this.ThumbnailUrl
			};
		}
	}
}
