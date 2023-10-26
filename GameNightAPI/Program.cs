using System.Text;
using Domain;
using DomainServices;
using GameNightAPI;
using GameNightAPI.Controllers;
using GraphQLServer.GraphQL;
using Infrastructure.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SQLData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "GameNight API", Version = "v1" });
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<GameNightDbContext>(x => x.UseSqlServer(connectionString));

var accountConnectionString = builder.Configuration.GetConnectionString("AccountDbContextConnection");
builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(accountConnectionString));

builder.Services.AddDefaultIdentity<GameNight2User>(options =>
{
	options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<NightQuery>();
builder.Services.AddScoped<INightRepository, NightEFRepository>();
builder.Services.AddScoped<IAccountRepository, AccountEFRepository>();

builder.Services
	.AddGraphQLServer()
	.AddQueryType<NightQuery>();

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
	options.TokenValidationParameters.ValidateAudience = false;
	options.TokenValidationParameters.ValidateIssuer = false;
	options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["BearerTokens:Key"]));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
