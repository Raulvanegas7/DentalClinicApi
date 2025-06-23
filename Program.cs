using DentalClinicApi.Configurations;
using DentalClinicApi.Contexts;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Add services to the container.

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<PatientService>();
builder.Services.AddSingleton<DentistService>();
builder.Services.AddSingleton<ServiceService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
