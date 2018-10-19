using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Soteria.Mobile.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeTokenAttribute : AuthorizationFilterAttribute
    {

        AuthorizationToken authorizationToken;

        public string Token;


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token = actionContext.Request.Headers.Authorization.Parameter;
            if (token != null)
            {
                string TokenID = Cryptography.Decrypt(token);
                string[] values = TokenID.Split(',');
                var authToken = new AuthorizationToken();
                authToken.UserID = Convert.ToInt32(values[0]);
                authToken.SchoolID = Convert.ToInt32(values[1]);
                authToken.SchoolDistrictID = Convert.ToInt32(values[2]);
                authToken.RoleID = Convert.ToInt32(values[3]);
                authToken.TokenGeneratedTimeStamp = Convert.ToDateTime(values[4]);

                TimeSpan ts = DateTime.UtcNow.Subtract(authToken.TokenGeneratedTimeStamp);
                if (ts.TotalMinutes > 1440)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
            }
        }
    }
}