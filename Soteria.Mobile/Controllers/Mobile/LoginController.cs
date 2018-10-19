using Soteria.DataComponents;
using Soteria.DataComponents.DataContext;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.Infrastructure.Common;
using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.DataComponents.ViewModel;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.Mobile.Auth;
using Soteria.Mobile.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Soteria.Mobile.Controllers
{
    [RoutePrefix("mobile/api")]
    public class LoginController : BaseApiController
    {
        [Route("login")]
        [HttpPost]
        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            var applicationUser = UserContext.Authenticate(authenticationRequest);

            if (applicationUser != null)
            {
                return new AuthenticationResponse()
                {
                    TokenID = JwtTokenManager.GenerateToken(new SessionToken()
                    {
                         RoleID = applicationUser.RoleID,
                         UserID = applicationUser.UserID,
                         SchoolDistrictID = applicationUser.SchoolDistrictID,
                         SchoolID = applicationUser.SchoolID
                    }),
                    ApplicationUser = applicationUser,
                    OperationResult = GetSuccessResult()
                };
            }
            else
            {
                return new AuthenticationResponse()
                {
                    OperationResult = GetFailureResult()
                };
            }
        }

        [Route("appconfig")]
        [HttpGet]
        [HeaderAuthentication(RoleType.SuperAdmin,RoleType.SchoolAdmin,RoleType.SRO)]
        public AppConfigResponse GetAppConfig()
        {
            var appConfig = UserContext.GetAppConfig(new AuthorizationToken()
            {
                UserID = CurrentSession.UserID,
                SchoolID = CurrentSession.SchoolID,
                SchoolDistrictID = CurrentSession.SchoolDistrictID,
                RoleID = CurrentSession.RoleID
            });
            if (appConfig != null)
            {
                return new AppConfigResponse()
                {
                    AppConfig = appConfig,
                    OperationResult = GetSuccessResult()
                };
            }
            else
            {
                return new AppConfigResponse()
                {
                    OperationResult = GetSuccessResult()
                };
            }
        }
    }
}
