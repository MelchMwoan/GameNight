using System.ComponentModel.DataAnnotations;
using Domain;
using DomainServices;

namespace GameNight2.Models
{
	public class NewSnackModel
	{
		public string Name { get; set; }
		public Boolean isVegan { get; set; }
		public Boolean isAlcoholFree { get; set; }
		public Boolean isVegatarian { get; set; }
		public Boolean isGlutenFree { get; set; }
		public Boolean isLactoseFree { get; set; }
		public Boolean isNutsFree { get; set; }
	}
}
