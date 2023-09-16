using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Persistence.Contexts;
using JobAdvertAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {

           ConfigurationManager configurationManager= new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/JobAdvertAPI.API"));
            configurationManager.AddJsonFile("appsettings.json");

            services.AddDbContext<JobAdvertContext>(options => options.UseSqlServer(configurationManager.GetConnectionString("SqlCon")) );

            services.AddScoped<IApplyStatusReadRepository, ApplyStatusReadRepository>();
            services.AddScoped<IApplyStatusWriteRepository, ApplyStatusWriteRepository>();
            services.AddScoped<IJobPostReadRepository, JobPostReadRepository>();
            services.AddScoped<IJobPostWriteRepository, JobPostWriteRepository>();
            services.AddScoped<IJobTypeReadRepository, JobTypeReadRepository>();
            services.AddScoped<IJobTypeWriteRepository, JobTypeWriteRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserJobPostReadRepository, UserJobPostReadRepository>();
            services.AddScoped<IUserJobPostWriteRepository, UserJobPostWriteRepository>();
            services.AddScoped<IUserTypeReadRepository, UserTypeReadRepository>();
            services.AddScoped<IUserTypeWriteRepository, UserTypeWriteRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IUserCvFileWriteRepository, UserCvFileWriteRepository>();
            services.AddScoped<IUserCvFileReadRepository, UserCvFileReadRepository>();
            services.AddScoped<IJobPostImageFileWriteRepository, JobPostImageFileWriteRepository>();
            services.AddScoped<IJobPostImageFileReadRepository, JobPostImageFileReadRepository>();
            
           
        }

    }
}
