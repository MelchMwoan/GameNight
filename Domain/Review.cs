namespace Domain
{
	public class Review
	{
		public int Id { get; set; }
		public double Rating { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Person Writer { get; set; }

	}
}
