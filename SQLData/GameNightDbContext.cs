using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SQLData
{
	public class GameNightDbContext : DbContext
	{
		public GameNightDbContext(DbContextOptions<GameNightDbContext> options) : base(options) { }

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<Night> Nights { get; set; }
		public DbSet<Person> Persons { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Snack> Snacks { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Address>()
				.HasData(
					new Address { Id = 1, City = "Breda", Street = "Lovensdijkstraat", HouseNumber = 63 }
				);

			modelBuilder.Entity<Person>()
				.HasData(
					new Person { Id = 1, Name = "Henk", RealName="Henk Man", Email = "henk@mail.nl", Gender = GenderEnum.Male, BirthDate = DateTime.Today.AddYears(-18), AddressId = 1 },
					new Person { Id = 2, Name = "Jan", RealName = "Jan Man", Email = "jan@mail.nl", Gender = GenderEnum.Other, BirthDate = DateTime.Today.AddYears(-23), AddressId = 1 }
				);
			modelBuilder.Entity<Night>()
				.HasOne(e => e.Organisator)
				.WithMany()
				.HasForeignKey(e => e.PersonId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.IsRequired();


			modelBuilder.Entity<Night>().HasData(
				new Night { Id = 1, DateTime = DateTime.Today.AddDays(7), PersonId = 1, MaxPlayers = 3 },
				new Night { Id = 2, DateTime = DateTime.Today.AddDays(3), PersonId = 2, MaxPlayers = 8 }
			);

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var connectionString = configuration.GetConnectionString("Default");
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}
