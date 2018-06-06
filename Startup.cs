using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;

using api_seed.Models;

namespace api_seed
{
    public class Startup
    {

        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.Configure<DBSettings>(options =>
            {
                options.ConnectionString 
                    = Configuration.GetSection("mongo_connection_development:ConnectionString").Value;
                    
                options.Database 
                    = Configuration.GetSection("mongo_connection_development:Database").Value;
            });

            // repositories
            services.AddTransient<IUserRepository, UserRepository>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) // export ASPNETCORE_ENVIRONMENT=development  
            {
                app.UseDeveloperExceptionPage();
                Console.Write("In development mode");
            }

            app.UseMvc();

            app.UseCors("CorsPolicy");

            app.Run(async (context) =>
            {
                // this means the request didn't pick up on 
                await context.Response.WriteAsync("Nothing to see here.");
            });
        }
    }

    public class DBSettings {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }


}
