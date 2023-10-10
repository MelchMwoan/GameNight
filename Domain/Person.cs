using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public class Person
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string RealName { get; set; }
		[Required(ErrorMessage = "Password is required")]
		public string Email { get; set; }

		public GenderEnum Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public int AddressId { get; set; }
		public Address Address { get; set; }
		public List<Game>? Games { get; set; }
		public List<Night>? Nights { get; set; }


		public void addGame(Game game) { Games.Add(game); }
		public void addNight(Night night) { Nights.Add(night); }

		public bool isPlayer() => Games != null;
		public bool isOrganisator() => Nights != null;
	}
}
