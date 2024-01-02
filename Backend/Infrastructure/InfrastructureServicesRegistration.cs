using Domain;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Application.Contracts;
using Application.Contracts.Services;
using System.Security.Claims;
using Infrastructure.Services.Events;
using Application.Events;
using Application.Events.Handlers;
using RabbitMQ.Client;

namespace Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        RoleClaimType = ClaimTypes.Role,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])
                        )
                    };
                }
            );
            services.AddAuthorization();
            
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IEthereumCryptoService, EthereumCryptoService>();

            // Register event handlers
            services.AddTransient<IEventHandler<ValueSetEvent>, ValueSetEventHandler>();

            services.AddSingleton<IConnection>(sp =>
                {
                    var factory = new ConnectionFactory
                    {
                        Uri = new Uri(configuration["RabbitMQ:ConnectionString"]),
                        AutomaticRecoveryEnabled = true
                    };
                    return  factory.CreateConnection();
                })
                .AddHealthChecks()
                .AddRabbitMQ();

            // Register message queue (e.g., RabbitMQ)
            services.AddSingleton<RabbitMqService>();

            // Register event emitting service and the processor
            services.AddTransient<PublisherService>();
            services.AddTransient<ConsumerService>();


            // Add hosted service for event queueing and processing
            services.AddHostedService(provider => provider.GetRequiredService<PublisherService>());
            services.AddHostedService(provider => provider.GetRequiredService<ConsumerService>());

            return services;
        }
    }
}
