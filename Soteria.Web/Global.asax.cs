using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Soteria.DataComponents.ViewModel.Auth;

namespace Soteria.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    string cookieUserdata = authTicket.UserData.ToString();
                    char[] splchar = { '¥' };
                    string[] token = cookieUserdata.Split(splchar);
                    UserSession UserSession = new UserSession(1);
                    JObject jObj = JObject.Parse(authTicket.UserData);
                    //userSession.RoleName = jObj["RoleName"].ToString();
                    //userSession.FullName = jObj["FullName"].ToString();
                    HttpContext.Current.User = UserSession;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
