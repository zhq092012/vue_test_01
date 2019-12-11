using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dataprovider.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

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
            //注入数据库上下文
            services.AddDbContext<MyDataDBContext>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));
            services.AddMvc(Options =>
            {
                Options.ReturnHttpNotAcceptable = true;//如果想要的格式不支持，那么就会返回406 Not Acceptable
            }).AddJsonOptions(Options =>
            {
                Options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                Options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                Options.SerializerSettings.ContractResolver = new DefaultContractResolver();//取消Json序列化的第一个字母小写
                Options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//忽略循环引用

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //注册MemoryCache缓存
            services.AddMemoryCache(setupAction =>
            {
                setupAction.ExpirationScanFrequency.Add(TimeSpan.FromMinutes(20));//缓存有效期
            });
            //解决跨域访问
            //string[] urls = Configuration.GetSection("AllowCors:AllowAllOrigin").Value.Split(',');
            services.AddCors(options => options.AddPolicy("Domain", builder => builder.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowCredentials()));
            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "zhanghq's API",
                    //Description = "A simple example ASP.NET Core Web API",
                    //TermsOfService = "None",
                    //Contact = new Contact
                    //{
                    //    Name = "zhanghq",
                    //    Email = string.Empty,
                    //    Url = "http://www.cnblogs.com/zhanghq/"
                    //},
                    //License = new License
                    //{
                    //    Name = "许可证名字",
                    //    Url = "http://www.cnblogs.com/zhanghq/"
                    //}
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "SwaggerDemo.xml");
                s.IncludeXmlComments(xmlPath);
            });
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
                //定义全局异常处理机制，但是这里要注意把环境变量ASPNETCORE_ENVIRONMENT的值改成Production（其实不是Development就可以）
                app.UseExceptionHandler(options =>
                {
                    options.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An Error Occurred.");
                    });
                });
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            //解决跨域访问
            app.UseCors("Domain");
            app.UseMvc();

            //使用swagger
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                s.RoutePrefix = string.Empty;//要在应用的根 (http://localhost:<port>/) 处提供 Swagger UI，请将 RoutePrefix 属性设置为空字符串
            });
        }
    }
}
