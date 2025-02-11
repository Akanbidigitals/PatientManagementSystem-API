using PatientManagementSystem_API.Middlewares;
using Serilog;

namespace PatientManagementSystem_API.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            //Register custom errorhandling middleware
            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            //Register serilog for logging
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);

              
            });
        }
    }
}
