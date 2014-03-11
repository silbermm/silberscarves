using SilberScarves.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SilberScarves
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            if(Request.IsAuthenticated){
                AccountService accountService = new AccountService();
                var user = accountService.findUser(Context.User.Identity.Name);
                if (user != null)
                {
                    Context.User = new System.Security.Principal.GenericPrincipal(
                        new System.Security.Principal.GenericIdentity(Context.User.Identity.Name),
                        user.Roles.Select(r => r.Name).ToArray());
                }
            }
       }
    }
}
