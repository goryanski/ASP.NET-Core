using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Practice2.Business.Implementation;
using Practice2.Business.Interfaces;
using Practice2.Helpers;
using Practice2.Middleware;
using Practice2.Models;

namespace Practice2
{
    public class Startup
    {
        UserValidator validator = new UserValidator();
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUsersService, UsersService>();
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUsersService usersService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.Map("/userAccount", (info) =>
            {
                info.UseMiddleware<AuthMiddleware>();
                info.Run(async (context) =>
                {
                    User user = context.Items["selectedUser"] as User;
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(usersService.GetuserAccountPage(user));
                });
            });

            app.Map("/registration", (info) =>
            {
                info.Run(async (context) =>
                {
                    context.Response.ContentType = "text/html";

                    if (context.Request.Method == "POST")
                    {
                        string userLogin = context.Request.Form["login"].ToString();
                        string userPassword = context.Request.Form["password"].ToString();

                        string response = validator.CheckUserData(userLogin, userPassword);
                        if(response.Equals("isValid")) {
                            response = usersService.TryAddUser(userLogin, userPassword);
                            if(response.Equals("success"))
                            {
                                context.Response.Redirect("/");
                            }
                            else
                            {
                                await context.Response.WriteAsync($"<h3>{response}</h3>");
                            }
                        }
                        else
                        {
                            await context.Response.WriteAsync($"<h3>{response}</h3>");
                        }
                    }
                    await context.Response.WriteAsync(usersService.GetAuthorizationPage());
                });
            });

            app.Map("/login", (info) =>
            {
                info.Run(async (context) =>
                {
                    context.Response.ContentType = "text/html";

                    if (context.Request.Method == "POST")
                    {
                        string userLogin = context.Request.Form["login"].ToString();
                        string userPassword = context.Request.Form["password"].ToString();

                        string response = usersService.CheckUserData(userLogin, userPassword);
                        if (response.Equals("success"))
                        {
                            User user = usersService.GetUserByLogin(userLogin);
                            // set cookie
                            context.Response.Cookies.Append(userLogin, user.Id, new CookieOptions
                            {
                                Expires = new DateTimeOffset(new DateTime(2030, 1, 1))
                            });

                            context.Response.Redirect("/");
                        }
                        else
                        {
                            await context.Response.WriteAsync($"<h3>{response}</h3>");
                        }
                    }
                    await context.Response.WriteAsync(usersService.GetAuthorizationPage());
                });
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(usersService.GetMainPage(context.Request));
                });
            });
        }
    }
}
