using FluentMigrator.Runner;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Valid.Test.Application.Amqp.Consumer;
using Valid.Test.Application.Amqp.Publisher;
using Valid.Test.Application.Behaviors;
using Valid.Test.Application.Mappers;
using Valid.Test.Application.Services;
using Valid.Test.Application.Services.Interfaces;
using Valid.Test.Domain.Notification;
using Valid.Test.Domain.Response;
using Valid.Test.Repository.Contexts;
using Valid.Test.Repository.Repositories;
using Valid.Test.Repository.Repositories.Interfaces;
using Valid.Test.UOW;
using Response = Valid.Test.Domain.Response.Response;

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
            AddRabbitMq(services, configuration);
            AddAuthentication(services, configuration);

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
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IProtocoloService, ProtocoloService>();
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

        public static void AddRabbitMq(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<GravarProtocoloConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:ConnectionString"]);

                    cfg.ReceiveEndpoint(configuration["RabbitMq:GravarProcolo:Queue"]!, e =>
                    {
                        e.ConfigureConsumer<GravarProtocoloConsumer>(context);
                        e.SetQueueArgument("x-queue-type", "quorum");
                        e.UseMessageRetry(r => r.Incremental(3, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)));
                        e.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(1)));
                        e.DiscardFaultedMessages();
                    });
                });
            });
        }
        
        public static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
        }
    }
}
