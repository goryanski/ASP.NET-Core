using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GetTravelApplication.Middleware
{
    public class IPVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _unavailableIps;
        public IPVerificationMiddleware(RequestDelegate next, List<string> unavailableIps)
        {
            _next = next;
            _unavailableIps = unavailableIps;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string userIP = context.Connection.RemoteIpAddress.ToString();

            if (_unavailableIps.Contains(userIP))
            {
                await context.Response.WriteAsync("<h1>Your IP address is unavailable!</h1>");
            }

            await _next.Invoke(context);
        }
    }
}
