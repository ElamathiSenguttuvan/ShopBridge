using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopbridge.Data;
using Swashbuckle.Swagger;
using Shopbridge.IRepository;
using Shopbridge.Repository;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
//using Shopbridge.UnitTest;
using Shopbridge.Controllers;

namespace Shopbridge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //var config = new ConfigurationBuilder()
            //   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //   .AddJsonFile("appsettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSession();
            services.AddScoped<IStockProcessRepository, StockProcessRepository>();
            services.AddDbContext<ShopBridgeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ShopBridgeDB")));
            //services.AddScoped<ShopBridgeContext>(provider => provider.GetService<ShopBridgeContext>());
            services.AddSwaggerGen();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseSession();
            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
