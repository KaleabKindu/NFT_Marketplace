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
using Microsoft.Extensions.Logging;

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

            #region ValueSet Event
            /***
                Register and host `ValueSet` event listening service
                to handle `ValueSet` events emitted from the smart contract
            ***/ 

            // Register and host valueset event listening service
            services.AddTransient<EventListeningService<ValueSetEvent>>();
            services.AddHostedService(provider => provider.GetRequiredService<EventListeningService<ValueSetEvent>>());
                        
            // Register and host valueset event processing service
            services.AddTransient<EventProcessingService<ValueSetEvent>>();
            services.AddHostedService(provider => provider.GetRequiredService<EventProcessingService<ValueSetEvent>>());
            
            #endregion ValueSet Event


            #region Other Event
            /***
                Register and host `Other` event listening service
                to handle `Other` events emitted from the smart contract
            ***/ 
            #endregion Other Event

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddConsole(); 
            });
            
            return services;
        }
    }
}
