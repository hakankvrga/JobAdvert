using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.Abstractions.Services.Authentications;
using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities.Identity;
using JobAdvertAPI.Persistence.Contexts;
using JobAdvertAPI.Persistence.Repositories;
using JobAdvertAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobAdvertAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {

       ConfigurationManager configurationManager= new();
        configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/JobAdvertAPI.API"));
        configurationManager.AddJsonFile("appsettings.json");

        services.AddDbContext<JobAdvertContext>(options => options.UseSqlServer(configurationManager.GetConnectionString("SqlCon")) );

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.User.AllowedUserNameCharacters = "abcçdefgğhijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIJKLMNOÖPQRSŞTUÜVWXYZ";
        }).AddEntityFrameworkStores<JobAdvertContext>();


        services.AddScoped<IApplyStatusReadRepository, ApplyStatusReadRepository>();
        services.AddScoped<IApplyStatusWriteRepository, ApplyStatusWriteRepository>();
        services.AddScoped<IJobPostReadRepository, JobPostReadRepository>();
        services.AddScoped<IJobPostWriteRepository, JobPostWriteRepository>();
       
       
        services.AddScoped<IUserJobPostReadRepository, UserJobPostReadRepository>();
        services.AddScoped<IUserJobPostWriteRepository, UserJobPostWriteRepository>();
      
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IUserCvFileWriteRepository, UserCvFileWriteRepository>();
        services.AddScoped<IUserCvFileReadRepository, UserCvFileReadRepository>();
        services.AddScoped<IJobPostImageFileWriteRepository, JobPostImageFileWriteRepository>();
        services.AddScoped<IJobPostImageFileReadRepository, JobPostImageFileReadRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();
       
       


    }

}
