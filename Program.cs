using DentalClinicApi.Configurations;
using DentalClinicApi.Contexts;
using DentalClinicApi.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Add services to the container.

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<PatientService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Dental Clinic API",
        Version = "v1",
        Description = "API REST para la gestión de pacientes en un consultorio odontológico",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Raúl Vanegas",
            Email = "raulvanegas711@gmail.com",
            Url = new Uri("https://github.com/Raulvanegas7")
        }
    });

    options.EnableAnnotations();
});

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
