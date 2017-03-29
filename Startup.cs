using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Model;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace invoicingSystem
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            
            
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SimpleInvoices.InvoiceContext>(options =>
    options.UseMySql(Configuration.GetConnectionString("mySqlConnection")));
            services.AddScoped<SimpleInvoices.Controllers.HomeController>();
            services.AddScoped<SimpleInvoices.Controllers.ValuesController>();
            services.AddScoped<SimpleInvoices.Controllers.UserController>();
            services.AddDirectoryBrowser();
            
           // services.AddScoped<SimpleInvoices.Controllers.AuditFormsController>();
            // Add framework services.
            services.AddMvc();
            /*Adding swagger generation with default settings*/
    services.AddSwaggerGen(options => {
        options.SingleApiVersion(new Info{
            Version="v1",
            Title="Auth0 Swagger Sample API",
            Description="API Sample made for Auth0",
            TermsOfService = "None"
        });
    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            
            /*Enabling swagger file*/
             app.UseSwagger();
             /*Enabling Swagger ui, consider doing it on Development env only*/
            app.UseSwaggerUi();
            app.UseDefaultFiles();
            app.UseStaticFiles();
           
       /*     app.UseMvc(routes =>
   {
       routes.MapRoute(
           name: "default",
           template: "{controller=Home}/{action=Index}");
   });*/
            app.UseMvc();

       
            
        }
    }
}
