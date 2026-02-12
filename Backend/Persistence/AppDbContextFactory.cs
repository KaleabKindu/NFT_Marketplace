using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence
{
    public class AppDbContextFactory:IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
 
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables("NFT_MARKET_")
                .Build();


            var builder = new DbContextOptionsBuilder<AppDbContext>();
            string connectionString = configuration["PostgreSQL:ConnectionString"];
            builder.UseNpgsql(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
