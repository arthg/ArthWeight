using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ArthWeight
{
    /*
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ArthwindsContext>
    {
        public ArthwindsContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ArthwindsContext>();
            IConfigurationRoot configuration =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", false)
            .Build();
            var connectionString = configuration.GetConnectionString("ArthwindsConnectionString");
            builder.UseSqlServer(connectionString);
            return new ArthwindsContext(builder.Options);
        }
    }
    */

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
               WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration(SetupConfiguration)
                   .UseStartup<Startup>()
                   .Build();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {           
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables();
        }
    }
}
