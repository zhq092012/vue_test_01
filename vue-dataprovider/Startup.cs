using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dataprovider.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ddataprovider
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
            services.AddDbContext<MyDataDBContext>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //解决跨域访问
            //string[] urls = Configuration.GetSection("AllowCors:AllowAllOrigin").Value.Split(',');
            services.AddCors(options => options.AddPolicy("Domain", builder => builder.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowCredentials()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            //解决跨域访问
            app.UseCors("Domain");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Values}/{action=get}/{id?}"
                    );
            });


        }
    }
}
