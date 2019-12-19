using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace AnimalAdoption.Service.Cart.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddControllers();
            services.AddTransient<CartLogic>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0", new OpenApiInfo { Title = "Cart API", Version = "v0" });
            });

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var exampleDbConnectionString = Configuration.GetValue<string>("ExampleDbConnectionString");
            var expectedConnectionString = "Server=myServerName;myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            // This example connection string is used for testing that variables have been passed in correctly.
            if ((exampleDbConnectionString ?? "") != expectedConnectionString)
            {
                app.Run(async (context) =>
                  await context.Response.WriteAsync($"Connection string not found or differs from expected value. \r\n Expected : {expectedConnectionString} \r\n Actual : {(string.IsNullOrWhiteSpace(exampleDbConnectionString) ? "Not found" : exampleDbConnectionString)}. \r\n Have you set the required application settings in your web app? You can look at 'appsettings.Development.json' for a guide."));
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v0/swagger.json", "Cart v0");
                c.RoutePrefix = string.Empty;
            });

            var failurePercentage = Configuration.GetValue<int?>("SimulatedFailureChance");
            if (failurePercentage != null)
            {
                var rand = new Random();
                app.Use(async (context, next) =>
                {
                    if (failurePercentage <= rand.Next(0, 100))
                    {
                        await next.Invoke();
                    }
                    else
                    {
                        throw new Exception($"A simulated failure occured - there is a {failurePercentage}% chance of this occuring");
                    }
                });
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
