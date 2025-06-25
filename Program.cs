using Microsoft.AspNetCore.Authentication.JwtBearer;
using DentalClinicApi.Configurations;
using DentalClinicApi.Contexts;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using DentalClinicApi.Swagger;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Add services to the container.

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<PatientService>();
builder.Services.AddSingleton<DentistService>();
builder.Services.AddSingleton<ServiceService>();
builder.Services.AddSingleton<AppointmentService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<JwtService>();


var jwtKey = builder.Configuration["Jwt:Key"];
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DentalClinicApi", Version = "v1" });

    // Configuración de JWT para Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese su token JWT como: Bearer {su token}"
    });

    // Solo aplica [Authorize] a los endpoints que lo tengan
    c.OperationFilter<AuthorizeCheckOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
