using Hangfire;
using Hangfire.Common;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace WebApplicationTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static int Autenticado { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            
            //services.AddHangfire(x => x.UseSqlServerStorage("<connection string>"));
            var inMemory = GlobalConfiguration.Configuration.UseMemoryStorage();
            services.AddHangfire(x => x.UseStorage(inMemory));
            JobStorage.Current = inMemory;


            services.AddMvc();

            RecurringJob.AddOrUpdate("Job Write por 1 minuto", () => Console.WriteLine("Recurring 1 Minuto!" + DateTime.Now), Cron.MinuteInterval(1));
            RecurringJob.AddOrUpdate("Job Write por 2 minuto", () => Console.WriteLine("Recurring 2 Minutos!" + DateTime.Now), Cron.MinuteInterval(2));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseAuthentication();

            app.UseHangfireServer();
           
            app.UseMvc();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HFDashboardAuthFilter() }
            });

           

        }


    }
}

