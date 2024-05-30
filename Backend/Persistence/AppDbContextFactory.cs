using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class AppDbContextFactory
    {
        public AppDbContext CreateDbContext(string[] args)
        {
 
            var configuration = new ConfigurationBuilder()
              .AddEnvironmentVariables("NFT_MARKET_")
              .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            string connectionString = configuration.GetConnectionString("AppConnectionString");
            builder.UseNpgsql(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
