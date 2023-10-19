using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SQLData;

public class AccountDbContext : IdentityDbContext<GameNight2User>
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
    {
    }

    public DbSet<GameNight2User> Accounts{ get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    modelBuilder.Entity<GameNight2User>().ToTable("Account");
	    base.OnModelCreating(modelBuilder);
    }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
			.Build();
		var connectionString = configuration.GetConnectionString("AccountDbContextConnection");
		//optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=GameNightAccounts;Trusted_Connection=True;");
		optionsBuilder.UseSqlServer(connectionString);
    }
}
