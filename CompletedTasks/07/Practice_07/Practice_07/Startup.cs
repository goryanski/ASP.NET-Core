using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Practice_07.Business.Services;
using Practice_07.DB;
using Practice_07.DB.Repositories;
using Practice_07.Services.Implementation;
using Practice_07.Services.Interfaces;
using Practice_07.UI.Services.Interfaces;
using Practice_07.UI.Services.Implementation;
using Practice_07.Business.Services.Implementation;
using Practice_07.Business.Services.Interfaces;

namespace Practice_07
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            
            services.AddDbContext<DatabaseContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IAuthDbService, AuthDbService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddTransient<INewsDbService, NewsDbService>();
            services.AddTransient<INewsService, NewsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller}/{action}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
