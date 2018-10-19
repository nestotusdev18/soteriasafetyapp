using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Soteria.DataComponents;
using Soteria.DataComponents.ViewModel.Auth;
using Soteria.DataComponents.Infrastructure;
using Soteria.Web.Helper;


namespace Soteria.Web.Controllers.View
{
    public class BaseViewController : Controller
    {
        public SessionToken CurrentUser
        {
            get { return HttpContext.User as SessionToken; }
        }
        public long? CurrentUserId { get; set; }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;
            ExceptionLogger.SaveLogFile(filterContext.Exception);
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
            Debug.WriteLine(filterContext.Exception);
            filterContext.ExceptionHandled = true;
        }
        public string GetEncryptedQueryString(object args)
        {
            var list = new RouteValueDictionary(args);
            IList<string> items = new List<string>();

            foreach (var entry in list)
            {
                if (entry.Value == null)
                    items.Add(string.Format("{0}{1}", entry.Key, "= "));
                else
                    items.Add(string.Format("{0}{1}{2}", entry.Key, "=", (!string.IsNullOrEmpty(entry.Value.ToString()) ? entry.Value : "")));
            }

            items.Add(string.Format("_Session={0}", Guid.NewGuid().ToString()));
            return BaseControllerHelper.EncryptValue(String.Join("&", items));
        }
        public string GetEncryptedQueryString()
        {
            IList<string> items = new List<string>();
            items.Add(string.Format("_Session={0}", Guid.NewGuid().ToString()));
            return BaseControllerHelper.EncryptValue(String.Join("&", items));
        }
        public virtual ActionResult RedirectToSafeAction(string action, string controller, object args)
        {
            var encText = GetEncryptedQueryString(args);
            return RedirectToAction(action, controller, new { q = encText });
        }
        public virtual ActionResult RedirectToSafeAction(string action, string controller)
        {
            var encText = GetEncryptedQueryString();
            return RedirectToAction(action, controller, new { q = encText });
        }
        public virtual ActionResult RedirectoSafeRoute(string routeName)
        {
            var encText = GetEncryptedQueryString();
            return RedirectToRoute(routeName, new { q = encText });
        }
        public IHtmlString SafeActionLink(string action, string controller, object args)
        {
            var encText = GetEncryptedQueryString(args);
            UrlHelper url = new UrlHelper(this.Request.RequestContext);
            return (new HtmlString(url.Action(action, controller, new { q = encText })));
        }
        public IHtmlString SafeActionLink(string action)
        {
            var encText = GetEncryptedQueryString();
            UrlHelper url = new UrlHelper(this.Request.RequestContext);
            return (new HtmlString(url.Action(action, new { q = encText })));
        }
        public IHtmlString SafeActionLink(string action, string controller)
        {
            var encText = GetEncryptedQueryString();
            UrlHelper url = new UrlHelper(this.Request.RequestContext);
            return (new HtmlString(url.Action(action, controller, new { q = encText })));
        }


    }
}