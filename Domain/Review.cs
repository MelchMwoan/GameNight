using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
	public class Review
	{
		public int Id { get; set; }
		public double Rating { get; set; }
		public string Description { get; set; }
		public int writerId { get; set; }
		public Person Writer { get; set; }
		public int organisatorId { get; set; }
		public Person Organisator { get; set; }
		public int nightId { get; set; }
		public Night night { get; set; }

		public void setWriter(Person person)
		{
			this.writerId = person.Id;
			this.Writer = person;
		}

		public void setOrganisator(Person organisator)
		{
			this.organisatorId = organisator.Id;
			this.Organisator = organisator;
		}

		public void setNight(Night night)
		{
			this.nightId = night.Id;
			this.night = night;
		}
	}
}
