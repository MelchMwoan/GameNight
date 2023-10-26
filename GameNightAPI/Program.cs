using DomainServices;
using GameNightAPI;
using GameNightAPI.Controllers;
using GraphQLServer.GraphQL;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using SQLData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextPool<GameNightDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<INightRepository, NightEFRepository>();

builder.Services
	.AddGraphQLServer()
	.AddQueryType<NightQuery>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
