using System.ComponentModel.DataAnnotations;
using Domain;
using DomainServices;

namespace GameNight2.Models
{
	public class NewSnackModel
	{
		public NewSnackModel(int nightId)
		{
			this.nightId = nightId;
		}
		public NewSnackModel()
		{
			this.nightId = nightId;
		}
		public int? nightId { get; set; }
		public string Name { get; set; }
		public Boolean isVegan { get; set; }
		public Boolean isAlcoholFree { get; set; }
		public Boolean isVegatarian { get; set; }
		public Boolean isGlutenFree { get; set; }
		public Boolean isLactoseFree { get; set; }
		public Boolean isNutsFree { get; set; }

		public Snack getSnack()
		{
			return new Snack()
			{
				Name = this.Name,
				isVegan = this.isVegan,
				isAlcoholFree = this.isAlcoholFree,
				isVegatarian = this.isVegatarian,
				isGlutenFree = this.isGlutenFree,
				isLactoseFree = this.isLactoseFree,
				isNutsFree = this.isNutsFree
			};
		}
	}
}
