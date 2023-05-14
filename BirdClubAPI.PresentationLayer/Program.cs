using BirdClubAPI.BusinessLayer.Configurations.AutoMapper;
using BirdClubAPI.Core.DependencyInjection;
using BirdClubAPI.PresentationLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDependencyInjection(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureSwaggerServices("BirdClub.APIs");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
