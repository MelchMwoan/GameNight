namespace GameNight2.Models
{
	public class Organisator : Person
	{
		public List<Night> Nights { get; set; }

		public void AddNight(Night night)
		{
			this.Nights.Add(night);
		}
	}
}
