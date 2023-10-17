using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Domain;

namespace GameNight2.Models
{
	public class NewUserModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public GenderEnum Gender { get; set; }
		public DateTime BirthDate { get; set; }

		public string City { get; set; }
		public string Street { get; set; }
		public int HouseNumber { get; set; }
		public Boolean isVegan { get; set; }
		public Boolean isAlcoholFree { get; set; }
		public Boolean isVegatarian { get; set; }
		public Boolean isGlutenFree { get; set; }
		public Boolean isLactoseFree { get; set; }
		public Boolean isNutsFree { get; set; }
	}
}
