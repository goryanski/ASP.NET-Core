using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Practice_19.Data;
using Practice_19.Data.UnitOfWork;
using Practice_19.Services;
using Practice_19.Utils;

namespace Practice_19
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
            services.AddSingleton<AppUtils>();
            services.AddLocalization(optns => optns.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddViewLocalization();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarsService, CarsService>();
            services.AddDbContextPool<ApplicationDbContext>(optns =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                optns.UseSqlServer(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            AppUtils utils)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                SupportedCultures = utils.AvailableCultures,
                SupportedUICultures = utils.AvailableCultures,
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(utils.DefaultCultureName),
            });

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
