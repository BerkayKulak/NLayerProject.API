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
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Service;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data;
using NLayerProject.Data.Repositories;
using NLayerProject.Data.UnitOfWorks;
using NLayerProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NLayerProject.API.Filters;
using NLayerProject.API.FÝlters;
using Microsoft.AspNetCore.Diagnostics;
using NLayerProject.API.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLayerProject.API.Extensions;
namespace NLayerProject.API
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



            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(),o=> {

                    o.MigrationsAssembly("NLayerProject.Data");
                });
            });

            services.AddAutoMapper(typeof(Startup));

            //constructrunda interface implemente ettiðimden dolayý
            services.AddScoped<NotFoundFilter>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();


            // bir request esnasýnda bir clasýn constructuronda IUnitOfWork ile karþýlaþýrsa 
            // gidicek UnitOfWork'ten bir nesne örneði alacak. Eðer bir request içersinde birden fazla
            // IUnitOfWork ile karþýlaþýrsa ayný nesne örneði üzerinden devam edicek.
            // bir request esnasýnda birden fazla ihtiyaç olursa ayný nesne örneðini yani ilk oluþturduðu nesne örneðini kullanýcak
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // her kontrollerde gidip bu validationFilteri yazmak istemiyorsam o zaman buraya geip
            // global olarak tüm controllerime eklemek istiyorsam
            services.AddControllers(o=> {

                o.Filters.Add(new ValidationFilter());
            });

            services.Configure<ApiBehaviorOptions>(options => {

                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NLayerProject.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NLayerProject.API v1"));
            }

            app.UseCustomException();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
