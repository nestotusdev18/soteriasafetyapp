using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Soteria.DataComponents.ViewModel.Auth;


namespace Soteria.TVApp.Auth
{
    public static class FormAuthManager
    {
        public static void AuthenticateNow(SessionToken userSession)
        {
            if (userSession == null)
            {
                throw new ArgumentNullException(nameof(userSession));
            }
            try
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, (new Guid()).ToString(), DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalSeconds), false, UserDataConstruct(userSession), FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.HttpOnly = true;
                authCookie.Secure = FormsAuthentication.RequireSSL;
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static void LogoutNow()
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            foreach (var cookie in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpContext.Current.Request.Cookies.Remove(cookie);
            }
        }
        internal static string UserDataConstruct(SessionToken userSession)
        {
            if (userSession == null)
            {
                throw new ArgumentNullException(nameof(userSession));
            }
            //string userdata = string.Empty;
            //string splchar = "¥";
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.SessionId, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.UserId, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.FullName, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.RoleId, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.RoleName, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.Metric, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.CustomerId, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.CustomerName, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.FacilityId, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.FacilityName, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.Email, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.Mobile, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.TimeZone, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.AppType, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.Initial, splchar);
            //userdata = string.Format("{0}{1}{2}", userdata, userSession.IdentityId, splchar);
            //return userdata;
            return JsonConvert.SerializeObject(userSession);
        }
        public static void UpdateMemberAuthenticate(SessionToken userSession)
        {
            if (userSession == null)
            {
                throw new ArgumentNullException(nameof(userSession));
            }

            try
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, (new Guid()).ToString(), DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalSeconds), false, UserDataConstruct(userSession), FormsAuthentication.FormsCookiePath);
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authNewCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    authCookie.HttpOnly = true;
                    authCookie.Secure = FormsAuthentication.RequireSSL;
                    foreach (HttpCookie item in HttpContext.Current.Response.Cookies)
                    {
                        HttpContext.Current.Response.Cookies.Remove(item.Name);
                    }
                    HttpContext.Current.Response.Cookies.Add(authNewCookie);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}