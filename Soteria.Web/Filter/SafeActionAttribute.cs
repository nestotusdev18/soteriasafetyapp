using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Soteria.DataComponents;
using Soteria.DataComponents.ViewModel.Auth;
using Soteria.DataComponents.Infrastructure;

namespace Soteria.Web.Filter
{
    /// <summary>
    /// SafeActionAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SafeActionAttribute : ActionFilterAttribute
    {
        #region Private Fields
        #endregion

        #region Properties
        /// <summary>
        /// To Do
        /// </summary>
        UserSession CurrentUserSession { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ControllerContext.HttpContext.User.Identity.IsAuthenticated) /*|| Request.UrlReferrer == null*/
            {
                CurrentUserSession = (UserSession)filterContext.Controller.ControllerContext.HttpContext.User;
            }

            if (filterContext.HttpContext.Request.HttpMethod != "POST")
            {
                Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
                if (HttpContext.Current.Request.QueryString.Get("q") != null)
                {
                    string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                    string decrptedString = Cryptography.Decrypt(encryptedQueryString.ToString());
                    string[] paramsArrs = decrptedString.Split('&');

                    for (int index = 0; index < paramsArrs.Length; index++)
                    {
                        string[] paramArr = paramsArrs[index].Split('=');
                        if (paramArr[0] != "_Session")
                            decryptedParameters.Add(paramArr[0], paramArr[1]);
                    }
                }

                for (int index = 0; index < decryptedParameters.Count; index++)
                {
                    var ActionInfo = filterContext.ActionDescriptor;
                    var pars = ActionInfo.GetParameters()[index];
                    var underlyingType = Nullable.GetUnderlyingType(pars.ParameterType);
                    if (underlyingType != null)
                    {
                        filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(index)] = Convert.ChangeType(decryptedParameters.Values.ElementAt(index), underlyingType ?? pars.ParameterType);
                    }
                    else
                    {
                        filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(index)] = Convert.ChangeType(decryptedParameters.Values.ElementAt(index), pars.ParameterType);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ChangeType<T>(object value)
        {
            var type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return default(T);
                type = Nullable.GetUnderlyingType(type);
            }
            return (T)Convert.ChangeType(value, type);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}