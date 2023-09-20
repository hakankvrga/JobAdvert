

using FluentValidation.AspNetCore;
using JobAdvertAPI.Aplication;
using JobAdvertAPI.Aplication.Validators.JobPosts;
using JobAdvertAPI.Infrastructure;
using JobAdvertAPI.Infrastructure.Filters;
using JobAdvertAPI.Infrastructure.Services.Storage.Azure;
using JobAdvertAPI.Infrastructure.Services.Storage.Local;
using JobAdvertAPI.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices();
builder.Services.AddAplicationServices();



//builder.Services.AddStorage(StorageType.Azure);
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateJobPostValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Employer", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true, // oluþturulacak tokeni kimler/origin/site kullanacak
        ValidateIssuer = true, // oluþturulacak token deðerini kimin daðýttýðýný ifade eden alan
        ValidateLifetime = true, // tokenin süresini kontrol eder
        ValidateIssuerSigningKey = true, // tokenin imzalý olup olmadýðýný kontrol eder

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
    };
});

builder.Services.AddCors(options=> options.AddDefaultPolicy(policy=>
policy.WithOrigins("https://localhost:4200/", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()

) );




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseCors();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
