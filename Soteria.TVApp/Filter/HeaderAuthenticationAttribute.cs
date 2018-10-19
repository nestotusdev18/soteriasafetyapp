using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using Soteria.DataComponents.ViewModel.Auth;
using Soteria.TVApp.Auth;
using Soteria.DataComponents.Infrastructure.Enum;

namespace Soteria.TVApp.Filter
{
        /// <summary>
        /// Header Authentication Attribute
        /// </summary>
        // [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
        public class HeaderAuthenticationAttribute : ActionFilterAttribute
        {
            #region Private Fields
            /// <summary>
            /// Authorized Roles Array
            /// </summary>
            List<RoleType> AuthorizedRoles { get; set; }
            bool EnableValidation { get; set; }
            #endregion

            #region Constructor
            public HeaderAuthenticationAttribute(bool enableValidation, params object[] args)
            {
                EnableValidation = enableValidation;
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
            public HeaderAuthenticationAttribute(params RoleType[] args)
            {
                EnableValidation = false;
                AuthorizedRoles = new List<RoleType>();
                if (args.Length > 0)
                {
                    foreach (var item in args)
                    {
                        AuthorizedRoles.Add((RoleType)Enum.Parse(typeof(RoleType), item.ToString()));
                    }
                }
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
                    if (!AuthorizedRoles.Contains(RoleType.Anonymous))
                    {
                        string TokenKey = "Authorization";
                        string tokenValue = string.Empty;
                        bool isValidRequst = false;
                         SessionToken SessionToken = new SessionToken();
                         UserSession UserSession = new UserSession(1);
                    if (filterContext.Request.Headers.Contains(TokenKey))
                        {
                            tokenValue = filterContext.Request.Headers.GetValues(TokenKey).FirstOrDefault();
                            if (string.IsNullOrEmpty(tokenValue))
                            {
                                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                            }
                            else
                            {
                                isValidRequst = ValidateToken(tokenValue.Replace("Bearer ", string.Empty), out SessionToken);
                                System.Web.HttpContext.Current.User = UserSession;
                            }
                        }
                        if (isValidRequst)
                        {
                            //if (!AuthorizedRoles.Contains(userSession.Metric))
                            //{
                            //    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Unauthorized Request" };
                            //    filterContext.Response = responseMessage;
                            //}
                        }
                        else
                        {
                            var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Unauthorized Request" };
                            filterContext.Response = responseMessage;
                        }
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
            /// <summary>
            /// Action Executed -> Log Write
            /// </summary>
            /// <param name="actionExecutedContext"></param>
            public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
            {
                base.OnActionExecuted(actionExecutedContext);
            }
            #endregion

            #region Private Methods
            /// <summary>
            /// Validate Token
            /// </summary>
            /// <param name="token"></param>
            /// <param name="userSession"></param>
            /// <returns></returns>
            private static bool ValidateToken(string token, out SessionToken SessionToken)
            {
                SessionToken = JwtManager.GetSessionToken(token);
                return (SessionToken != null);
            }
            #endregion
        }
    
}