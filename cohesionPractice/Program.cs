using cohesionPractice.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionPractice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("intializing app.....");
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope()) 
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<ServiceRequestContext>();
                        context.Database.EnsureDeleted();
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex, "An Erorr has occured while migrating the database");
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped working because of execption");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
            
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseNLog();
                });
    }
}
