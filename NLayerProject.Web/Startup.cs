using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NLayerProject.Web.Filters;
using NLayerProject.Web.ApiService;

namespace NLayerProject.Web
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
            // içine giricem çünkü baseUrl tanýmlýcam
            services.AddHttpClient<CategoryApiService>(options =>
            {
                options.BaseAddress = new Uri(Configuration["baseUrl"]);
            });

            services.AddScoped<NotFoundFilter>();
            
            services.AddAutoMapper(typeof(Startup));

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o => {

            //        o.MigrationsAssembly("NLayerProject.Data");
            //    });
            //});


            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
