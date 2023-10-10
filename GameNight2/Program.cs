using Domain;
using DomainServices;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using SQLData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<GameNightDbContext>(x => x.UseSqlServer(connectionString));

var accountConnectionString = builder.Configuration.GetConnectionString("AccountDbContextConnection");
builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(accountConnectionString));

builder.Services.AddDefaultIdentity<GameNight2User>(options =>
{
	options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AccountDbContext>();

builder.Services.AddScoped<INightRepository, NightEFRepository>();
builder.Services.AddScoped<IAccountRepository, AccountEFRepository>();

//builder.Services.AddAuthentication().AddIdentityServerJwt();

builder.Services.AddRazorPages();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
