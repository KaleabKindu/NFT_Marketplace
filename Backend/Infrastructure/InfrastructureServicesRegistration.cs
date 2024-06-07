using Domain;
using Hangfire;
using Persistence;
using System.Text;
using RabbitMQ.Client;
using Hangfire.PostgreSql;
using Application.Contracts;
using System.Security.Claims;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Application.Contracts.Services;
using Application.Features.Bids.Dtos;
using Application.Features.Assets.Dtos;
using Application.Features.Auctions.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            services.AddScoped<IAuctionManagementService, AuctionManagementService>();
            services.AddSingleton(sp =>
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

            services.AddSingleton(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                var queues = new List<string>{
                    $"{typeof(AuctionCreatedEventDto)}", $"{typeof(BidPlacedEventDto)}", $"{typeof(AuctionEndedEventDto)}", 
                    $"{typeof(AssetSoldEventDto)}", $"{typeof(ResellAssetEventDto)}", $"{typeof(TransferAssetEventDto)}", 
                    $"{typeof(DeleteAssetEventDto)}"
                };
                return new RabbitMqService(connection, queues);
            });

            services.AddTransient<EventListeningService>();
            services.AddHostedService(provider => provider.GetRequiredService<EventListeningService>());
                        
            services.AddTransient<EventProcessingService>();
            services.AddHostedService(provider => provider.GetRequiredService<EventProcessingService>());

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddConsole(); 
            });

            services.AddHangfire(options => options.UsePostgreSqlStorage(configuration["PostgreSQL:ConnectionString"]));
            services.AddHangfireServer();
            
            return services;
        }
    }
}
