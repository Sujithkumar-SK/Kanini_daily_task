using APIKanini.Service;
using Hospital_Management.Data;
using Hospital_Management.Interface;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Hospital_Management.Repository;
using Hospital_Management.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConn")));
builder.Services.Configure<User>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RoleClaimType = ClaimTypes.Role,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", Policy =>
    {
        Policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
// Doctors
builder.Services.AddScoped<IHospitalAPI<Doctor>, DoctorRepository>();
builder.Services.AddScoped<DoctorService>();

// Hospitals
builder.Services.AddScoped<IHospitalAPI<Hospital>, HospitalRepository>();
builder.Services.AddScoped<HospitalService>();

// Patients
builder.Services.AddScoped<IHospitalAPI<Patient>, PatientRepository>();
builder.Services.AddScoped<PatientService>();


// Register Repositories
builder.Services.AddScoped<IHospitalAPI<User>, UserRepository>();
builder.Services.AddScoped<IUser, UserRepository>();

// Register UserService
builder.Services.AddScoped<UserService>();

// ✅ Register PasswordHasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// Token + Auth
builder.Services.AddScoped<IToken, TokenService>();
builder.Services.AddScoped<IUser, UserRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAngular");

app.MapControllers();

app.Run();
