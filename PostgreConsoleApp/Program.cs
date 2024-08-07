using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PostgreConsoleApp.DataAccess;

namespace PostgreConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var appRunner = ActivatorUtilities.CreateInstance<AppRunner>(host.Services);
        await appRunner.RunAsync(args);
    }

    private static IHostBuilder CreateHostBuilder(string[] args) => 
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, builder) => builder.AddUserSecrets<Program>())
            .ConfigureServices((context, services) => 
            {
                services.AddTransient<AppRunner>();
                services.AddDbContext<MonitorDbContext>(options => {
                    options.UseNpgsql(context.Configuration.GetConnectionString("ChangeMonitor"));
                    options.UseSnakeCaseNamingConvention();
                });
            });
}