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

			modelBuilder.Entity<Night>()
				.HasOne(e => e.Organisator)
				.WithMany()
				.HasForeignKey(e => e.PersonId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.IsRequired();

			modelBuilder.Entity<Review>()
				.HasOne(e => e.Organisator)
				.WithMany()
				.HasForeignKey(e => e.organisatorId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.IsRequired();

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
				.Build();
			var connectionString = configuration.GetConnectionString("Default");
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}
