using System;
using AutoMapper;
using Cs.Application.Tools;
using Cs.Common.Tools.Security;
using Cs.Persistence.Tools;
using Cs.Service.Configuration.Filters;
using Cs.Service.Configuration.Middlewares;
using Cs.Service.Configuration.Swagger;
using Cs.Service.Configuration.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Cs.Service
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
            services
                .AddAutoMapper()
                .AddCors()
                .AddMvc()
                .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    }
                );

            services.AddAuthentication();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Construction Soft Api", Version = "v1"});
                c.OperationFilter<AuthorizationHeaderOperationFilter>();
            });

            services.AddApplicationServices().AddPersistenceServices(Configuration.GetConnectionString("MsSql"));

            services.CreateUserAtStartUp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/{Date}.txt", LogLevel.Error);

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseStaticFiles();

            app.UseMiddleware<LogAndExceptionHandler>();
            
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "Construction Soft Api V1"); });

            app.UseMvc();
        }
    }
}