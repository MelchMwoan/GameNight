namespace Domain
{
	public class Review
	{
		public int Id { get; set; }
		public double Rating { get; set; }
		public string Description { get; set; }
		public int writerId { get; set; }
		public Person Writer { get; set; }
		public int nightId { get; set; }
		public Night night { get; set; }

	}
}
