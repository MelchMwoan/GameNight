using Domain;
using System.ComponentModel.DataAnnotations;
using DomainServices;

namespace GameNight2.Models
{
	public class EditNightModel : NewNightModel
	{
		public int Id { get; set; }


		public Night getNight()
		{
			return new Night
			{
				Id = this.Id,
				Title = this.Title,
				DateTime = this.DateTime,
				MaxPlayers = this.MaxPlayers,
				ThumbnailUrl = this.ThumbnailUrl
			};
		}
	}
}
