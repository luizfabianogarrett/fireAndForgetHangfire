using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApplicationTask
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //GlobalConfiguration.Configuration.UseMemoryStorage();
    
            BuildWebHost(args).Run();
          
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
