using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using cohesionPractice.Services;
using cohesionPractice.Contexts;
using AutoMapper;

namespace cohesionPractice
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //added xml out put for compatability best practices
            services.AddMvc(options => options.EnableEndpointRouting = false).AddMvcOptions(o =>
            {
                o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

            }
            );

            services.AddTransient< IFakeMailService, FakeMailService >();
            var connectionString = _configuration["ConnectionStrings:ServiceRequestDBString"];
            services.AddDbContext<ServiceRequestContext>( o => 
            {
                o.UseSqlServer(connectionString);
            });
            services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("");
            }

            app.UseStatusCodePages();
            //status code page 
            app.UseStatusCodePages();

            //use mvc controler to use service requerst model
            app.UseMvc();


           
        }
    }
}
