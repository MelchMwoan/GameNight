using GameNight2.Models;
using Microsoft.EntityFrameworkCore;

namespace GameNight2.Data
{
	public class GameNightDbContext : DbContext
	{
		public GameNightDbContext(DbContextOptions<GameNightDbContext> options) : base(options) { }

		DbSet<Address> Addresses { get; set; }
		DbSet<Game> Games { get; set; }
		DbSet<Night> Nights { get; set; }
		DbSet<Organisator> Organisators { get; set; }
		DbSet<Player> Players { get; set; }
		DbSet<Review> Reviews { get; set; }
		DbSet<Snack> Snacks { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>().ToTable("Person");
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var connectionString = configuration.GetConnectionString("Default");
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}
