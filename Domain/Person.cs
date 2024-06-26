﻿using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public class Person
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string RealName { get; set; }
		[Required(ErrorMessage = "Password is required")]
		public string Email { get; set; }

		public string pfpUrl { get; set; } = "https://t4.ftcdn.net/jpg/00/64/67/27/360_F_64672736_U5kpdGs9keUll8CRQ3p3YaEv2M6qkVY5.jpg";
		public GenderEnum Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public int AddressId { get; set; }
		public Address Address { get; set; }
		public List<Game>? Games { get; set; }
		public List<Night>? Nights { get; set; }
		public Boolean isVegan { get; set; }
		public Boolean isAlcoholFree { get; set; }
		public Boolean isVegatarian { get; set; }
		public Boolean isGlutenFree { get; set; }
		public Boolean isLactoseFree { get; set; }
		public Boolean isNutsFree { get; set; }


		public void addGame(Game game) { Games.Add(game); }
		public void addNight(Night night) { Nights.Add(night); }

		public void SetBirthdate(DateTime birthdate)
		{
			if (birthdate > DateTime.Now) throw new Exception("Your birthdate can't be in the future");
			if (birthdate > DateTime.Now.AddYears(-16)) throw new Exception("You need to be at least 16 years to register");
			this.BirthDate = birthdate;
		}
	}
}
