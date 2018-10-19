using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.Mobile.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Soteria.Mobile.Filter
{
    /// <summary>
    /// Header Authentication Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class HeaderAuthenticationAttribute : ActionFilterAttribute
    {
        #region Private Fields
        /// <summary>
        /// Authorized Roles Array
        /// </summary>
        List<RoleType> AuthorizedRoles { get; set; }
        #endregion

        #region Constructor
        public HeaderAuthenticationAttribute(params object[] args)
        {
            AuthorizedRoles = new List<RoleType>();
            if (args.Length > 0)
            {
                foreach (var item in args)
                {
                    AuthorizedRoles.Add((RoleType)Enum.Parse(typeof(RoleType), item.ToString()));
                }
            }
        }
        /// <summary>
        /// HeaderAuthenticationAttribute
        /// </summary>
        /// <param name="args"></param>
        public HeaderAuthenticationAttribute()
        {
            AuthorizedRoles = new List<RoleType>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// On Base Action Executing
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            try
            {
                string TokenKey = "Authorization";
                string tokenValue = string.Empty;
                bool isValidRequst = false;
                SessionToken sessionToken = new SessionToken();
                if (filterContext.Request.Headers.Contains(TokenKey))
                {
                    tokenValue = filterContext.Request.Headers.GetValues(TokenKey).FirstOrDefault();

                    if (string.IsNullOrEmpty(tokenValue))
                    {
                        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        isValidRequst = ValidateToken(tokenValue.Replace("Bearer ", string.Empty), out sessionToken);
                        System.Web.HttpContext.Current.User = new UserSession(sessionToken);
                    }

                    if(AuthorizedRoles.Count() > 0 && sessionToken.RoleID != 0)
                    {
                        RoleType roleType = (RoleType)Enum.Parse(typeof(RoleType), sessionToken.RoleID.ToString());
                        isValidRequst = AuthorizedRoles.Contains(roleType);
                    }
                }
                if (!isValidRequst)
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Unauthorized Request" };
                    filterContext.Response = responseMessage;
                }
                if (filterContext.ModelState.IsValid == false)
                {
                    filterContext.Response = filterContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, filterContext.ModelState);
                }
                base.OnActionExecuting(filterContext);
            }
            catch (Exception exception)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Unauthorized Request" };
                filterContext.Response = responseMessage;
                throw exception;
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="sessionToken"></param>
        /// <returns></returns>
        private static bool ValidateToken(string token, out SessionToken sessionToken)
        {
            sessionToken = JwtTokenManager.GetSessionToken(token);
            return (sessionToken != null);
        }
        #endregion
    }
}