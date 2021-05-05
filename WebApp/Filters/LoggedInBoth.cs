using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Filters
{
    public class LoggedInBoth: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Filters.OfType<NotLoggedIn>().Any())
            {
                return; //ako ima takav filter nemoj da izvrsavas ovaj
            }
            if (context.HttpContext.Session.GetInt32("administratorid") == null && context.HttpContext.Session.GetInt32("korisnikid") == null)
            {
                context.HttpContext.Response.Redirect("/Korisnik/Index");
                return;
            }
        }
    }
}
