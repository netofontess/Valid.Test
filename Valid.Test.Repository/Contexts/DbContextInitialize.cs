using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Valid.Test.Repository.Contexts
{
    public static class DbContextInitialize
    {
        public static IServiceCollection AddDataBaseSqlServer(this IServiceCollection services, string connectionString)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddDbContext<ValidContext>(options =>
                options.UseSqlServer(connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assemblyName);
                }));

            return services;
        }
    }
}
