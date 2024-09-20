using FluentMigrator.Runner;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Valid.Test.Application.Behaviors;
using Valid.Test.Application.Mappers;
using Valid.Test.Domain.Notification;
using Valid.Test.Domain.Response;
using Valid.Test.Repository.Contexts;
using Valid.Test.Repository.Repositories;
using Valid.Test.Repository.Repositories.Interfaces;
using Valid.Test.UOW;

namespace Valid.Test.IOC
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnection = configuration["ConnectionStrings:DefaultConnection"]!;

            services.AddDataBaseSqlServer(defaultConnection);
            ConfigAutoMapper(services);
            AddDependencyService(services);
            AddDependencyRepository(services);
            AddMediatRAndValidators(services);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(defaultConnection)
                    .ScanIn(typeof(DbContextInitialize).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        private static void ConfigAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PerfilMapeamento).Assembly);
        }

        private static void AddDependencyService(IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();
            services.AddScoped<IResponse, Response>();
        }

        private static void AddDependencyRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProtocoloRepository, ProtocoloRepository>();
        }

        private static void AddMediatRAndValidators(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                AssemblyScanner
                    .FindValidatorsInAssembly(assembly)
                    .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly)); 
            }
        }
    }
}
