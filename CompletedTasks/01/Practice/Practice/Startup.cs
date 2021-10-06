using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Practice.App;
using Practice.App.Models;

namespace Practice
{
    public class Startup
    {
        UsersStorage usersStorage = UsersStorage.Instance;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();


            app.Map("/userInfo", (info) => 
            {
                User user = null;
                info.Use(async (context, next) =>
                {
                    string userId = context.Request.Query["id"].ToString();
                    user = usersStorage.Users.FirstOrDefault(u => u.Id == userId);
                    if(user is null)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Id doesn`t exists");
                    }
                    else
                    {
                        await next.Invoke();
                    }
                    
                });
                info.Run(async (context) =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(GetUserInfoHtml(user));
                });
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(GetUsersList(context.Request.Host.Value));
                });
            });
        }

        private string GetUsersList(string host)
        {
            StringBuilder list = new StringBuilder();
            list.Append("<ul>");
            for (int i = 0; i < usersStorage.Users.Count; i++)
            {
                string detailsLink = $"<a href=\"http://{host}/userInfo?id={usersStorage.Users[i].Id}\">more...</a>";
                list.Append($"<li>{usersStorage.Users[i]}\t{detailsLink}</li>");
            }
            list.Append("</ul>");
            return list.ToString();
        }

        private string GetUserInfoHtml(User user)
        {
            return $"<h3>User FirstName: {user.FirstName}<br>" +
                $"User LastName: {user.LastName}<br>" +
                $"User Age: {user.Age}<br>" +
                $"User Id: {user.Id}</h3>";
        }
    }
}
