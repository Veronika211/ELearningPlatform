using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CheckIfKorisnikIsLoggedInMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckIfKorisnikIsLoggedInMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            int? id = httpContext.Session.GetInt32("korisnikid");
            if (id == null && httpContext.Request.Path!="/Korisnik/Index")
            {
                httpContext.Response.Redirect("/Korisnik/Index"); //ako nije prijavljen prebaci ga na index
            }
            Console.WriteLine(id);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CheckIfKorisnikIsLoggedInMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckIfKorisnikIsLoggedInMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckIfKorisnikIsLoggedInMiddleware>();
        }
    }
}
