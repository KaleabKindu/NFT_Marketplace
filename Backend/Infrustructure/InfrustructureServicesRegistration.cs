using Domain;
using Infrustructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Application.Contracts;

namespace Infrustructure
{
    public static class InfrustructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrustructureServices(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>();


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(opt =>
            {
               
            });
            services.AddScoped<TokenService>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            return services;
        }
    }
}
