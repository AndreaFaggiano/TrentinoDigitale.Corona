using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace TrentinoDigitale.Corona.Mvc
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

        //protected void Session_Start(object sender, EventArgs e) 
        //{
        //    FormsAuthentication.SetAuthCookie("pippo", false);

        //    GenericIdentity identity = new GenericIdentity("pippo");
        //    GenericPrincipal principal = new GenericPrincipal(identity, 
        //        new string[] { });
        //    Thread.CurrentPrincipal = principal;

        //    string ss = "";
        //}

        //public void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    GenericIdentity identity = new GenericIdentity("pippo");
        //    GenericPrincipal principal = new GenericPrincipal(identity,
        //        new string[] { });
        //    HttpContext.Current.User = principal;
        //    Thread.CurrentPrincipal = HttpContext.Current.User;

        //    //FormsAuthentication.SetAuthCookie("pippo", false);
        //}
    }
}
