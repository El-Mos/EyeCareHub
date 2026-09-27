using AutoMapper;
using AutoMapper.Configuration;
using EyeCareHub.API.Dtos.OrderDtos;
using EyeCareHub.API.Extensions;
using EyeCareHub.API.Helper;
using EyeCareHub.API.Middlewares;
using EyeCareHub.DAL.Data;
using EyeCareHub.DAL.Entities.OrderAggregate;
using EyeCareHub.DAL.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EyeCareHub.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EyeCareHub.API", Version = "v1" });
            });
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));   //singletone
            });

            
            services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                //var connect = Configuration.GetSection("BasketSettings:Redis").Value;

                //return ConnectionMultiplexer.Connect(connect);

                var config = Configuration.GetSection("RedisSettings");

                var options = new ConfigurationOptions
                {
                    EndPoints = { { config["Host"], int.Parse(config["Port"]) } },
                    User = config["User"],
                    Password = config["Password"],
                    //AbortOnConnectFail = false,
                    //Ssl = true 
                };

                return ConnectionMultiplexer.Connect(options);
            });

            /*
    {
        var muxer = ConnectionMultiplexer.Connect(
            new ConfigurationOptions{
                EndPoints= { {"redis-13477.c82.us-east-1-2.ec2.redns.redis-cloud.com", 13477} },
                User="default",
                Password="y3qtW0K0M4aPdFsx9of2oEFGNfy3cOoM"
            }
        );
        var db = muxer.GetDatabase();
        
        db.StringSet("foo", "bar");
        RedisValue result = db.StringGet("foo");
        Console.WriteLine(result); // >>> bar
        
    }
             */
            services.AddIdentityServices(Configuration);
            services.AddApplicationServices();

            services.AddControllers()
                .AddJsonOptions(options =>
                     {
                         options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
                         options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
                     });

            //  services.AddScoped<IValueResolver<OrderItem, OrderItemDto, string>, ProductItemOrderPictureURLResolver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EyeCareHub.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
