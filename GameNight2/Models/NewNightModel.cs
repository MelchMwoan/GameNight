using System.ComponentModel.DataAnnotations;
using Domain;
using DomainServices;

namespace GameNight2.Models
{
	public class NewNightModel
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateTime DateTime { get; set; }
		[Range(2, 100, ErrorMessage = "MaxPlayers should be at least 2")]
		public int MaxPlayers { get; set; }
		[Url]
		public string ThumbnailUrl { get; set; }

		public Boolean AdultOnly { get; set; }
		[Required(ErrorMessage = "Select at least 1 game")]
		public List<int> SelectedGames { get; set; }
		public List<int> SelectedSnacks { get; set; } = new List<int>();

		public Boolean TakeOwnSnacks { get; set; }
	}
}
