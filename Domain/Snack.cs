﻿namespace Domain
{
	public class Snack
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int personId { get; set; }
		public Person person { get; set; }
		public int? nightId { get; set; }
		public Night? night { get; set; }
		public Boolean isVegan { get; set; }
		public Boolean isAlcoholFree { get; set; }
		public Boolean isVegatarian { get; set; }
		public Boolean isGlutenFree { get; set; }
		public Boolean isLactoseFree { get; set; }
		public Boolean isNutsFree { get; set; }

		public void SetNight(Night night)
		{
			this.nightId = night.Id;
			this.night = night; 
		}
	}
}
