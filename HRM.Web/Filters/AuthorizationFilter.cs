using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Web.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userid = Convert.ToString(context.HttpContext.Session.GetString("UserId"));

            if (!string.IsNullOrEmpty(userid))
            {
                if (userid != "admin@gmail.com")
                {
                    context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "Unauthorize",
                    controller = "Home"
                }));

                }
                
            }
            else
            {
                context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "Unauthorize",
                    controller = "Home"
                }));

            }
        }
    }
}
