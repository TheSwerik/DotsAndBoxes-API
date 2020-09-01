using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IdentityModelEventSource.ShowPII = true;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}