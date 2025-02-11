using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem_API.DataAccess.DataContext;
using PatientManagementSystem_API.DataAccess.Interface;
using PatientManagementSystem_API.DataAccess.Repository;

namespace PatientManagementSystem_API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddExtension(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection"); //Get connection string from appsettings

            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly; // Assigning and registering the assembly for validations

            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString)); // Inhrcting our dbcontects

            services.AddScoped<IPatientRepository, PatientRepository>(); // Dependency Injection for Patients

            services.AddScoped<IPatientRecord, PatientRecordRepository>(); // D.I for patients Record

            services.AddMemoryCache(); // Register Cache memory

            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
            
        }
    }
}
