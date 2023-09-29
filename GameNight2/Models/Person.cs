namespace GameNight2.Models
{
	public abstract class Person
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }

		public GenderEnum Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public Address Address { get; set; }

	}
}
