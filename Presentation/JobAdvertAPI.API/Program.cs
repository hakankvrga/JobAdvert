

using FluentValidation.AspNetCore;
using JobAdvertAPI.API.Configurations.ColumnWriter;
using JobAdvertAPI.Aplication;
using JobAdvertAPI.Aplication.Validators.JobPosts;
using JobAdvertAPI.Infrastructure;
using JobAdvertAPI.Infrastructure.Filters;
using JobAdvertAPI.Infrastructure.Services.Storage.Azure;
using JobAdvertAPI.Infrastructure.Services.Storage.Local;
using JobAdvertAPI.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Text;
using JobAdvertAPI.API.Extensions;

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
        LifetimeValidator = (before, expires, token, param) => expires != null ? expires > DateTime.UtcNow : false,

        NameClaimType = ClaimTypes.Name

    };
});

builder.Services.AddCors(options=> options.AddDefaultPolicy(policy=>
policy.WithOrigins("https://localhost:4200/", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()

) );

SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "Email";
sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
sqlColumn.PropertyName = "Email";
sqlColumn.DataLength = 50;
sqlColumn.AllowNull = true;
ColumnOptions columnOpt = new ColumnOptions();
columnOpt.Store.Remove(StandardColumn.Properties);
columnOpt.Store.Add(StandardColumn.LogEvent);
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

Logger log = new LoggerConfiguration()
.WriteTo.Console()
.WriteTo.File("logs/log.txt")
.WriteTo.MSSqlServer(
connectionString: builder.Configuration.GetConnectionString("SqlCon"),
sinkOptions: new MSSqlServerSinkOptions
{
    AutoCreateSqlTable = true,
    TableName = "Logs",
},
appConfiguration: null,
columnOptions: columnOpt
)
.WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
.Enrich.FromLogContext()
.Enrich.With<CustomUserNameColumn>()
.MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

var email = "user@example.com";
log.Information("Bu bir bilgi logu. Email: {Email}", email);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpLogging();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("UserName", username);
    await next();
});

app.MapControllers();

app.Run();
