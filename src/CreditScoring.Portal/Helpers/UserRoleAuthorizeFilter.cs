using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace CreditScoring.Portal.Helpers
{
    public class UserRoleAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.User != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                var role = context.HttpContext.User.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).ToList().FirstOrDefault().Value;
                if(role == "Admin")
                {
                    context.HttpContext.Response.Redirect("/users");
                    return;
                }
                if (role == "User")
                {
                    return;
                }

                
            }
            context.HttpContext.Response.Redirect("/login");
            return;
            //context.HttpContext.
        }
    }
}
