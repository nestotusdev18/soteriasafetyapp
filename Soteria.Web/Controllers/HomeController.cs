using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Soteria.DataComponents.ViewModel.Auth;
using Soteria.Web.Controllers.View;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.DataContext;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.Web.Auth;
using Soteria.DataComponents.DataModel;
using Soteria.Web.Filter;

namespace Soteria.Web.Controllers
{
    public class HomeController : BaseViewController
    {
        [Route("home", Name = "AppHome")]
        [SafeActionAttribute]
        public ActionResult Index(SearchCriteria searchCriteria)
        {
            try
            {
                ApplicationUser userSession = (ApplicationUser)Session["UserSession"];
                if (userSession == null)
                {
                    return RedirectoSafeRoute("AppLogin");
                }
                ViewBag.Title = "Home Page";
                ViewBag.HeaderToken = Session["HeaderToken"];
                ViewBag.UserName = Session["UserName"];
                ViewBag.UserID = userSession.UserID;
                ViewBag.SchoolID = userSession.SchoolID;
                ViewBag.SchoolCode = userSession.SchoolCode;
                RoleType RoleName = (RoleType)Enum.Parse(typeof(RoleType), userSession.RoleName);
                ViewBag.RoleName = RoleName;
                return View();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, 9999, 11, ExceptionSeverity.Severity);
                return RedirectoSafeRoute("AppLogin");
            }
        }

        [HttpGet]
        [Route("app/logout")]
        public ActionResult Logout()
        {
            FormAuthManager.LogoutNow();
            return RedirectoSafeRoute("AppLogin");
        }

        [HttpGet]
        [Route("app/login")]
        [Route("~/")]
        [SafeActionAttribute]
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [Route("app/login", Name = "AppLogin")]
        public ActionResult Login(AuthenticationRequest authenticationRequest)
        {
            ApplicationUser applicationUser = UserContext.Authenticate(authenticationRequest);

            if (applicationUser != null) {

                var TokenID = JwtManager.GenerateToken(new SessionToken()
                {
                    RoleID = applicationUser.RoleID,
                    UserID = applicationUser.UserID,
                    SchoolDistrictID = applicationUser.SchoolDistrictID,
                    SchoolID = applicationUser.SchoolID
                });

                Session["HeaderToken"] = TokenID;
                Session["UserSession"] = applicationUser;
                Session["UserName"] = applicationUser.FirstName;

                FormAuthManager.AuthenticateNow(new SessionToken()
                {
                    RoleID = applicationUser.RoleID,
                    UserID = applicationUser.UserID,
                    SchoolDistrictID = applicationUser.SchoolDistrictID,
                    SchoolID = applicationUser.SchoolID
                });

                return RedirectoSafeRoute("AppHome");
            }
            else
            {
                ModelState.AddModelError("loginsummary", "User Authentication Fails, Username and/or Password doesn't work");
            }

            return View(authenticationRequest);

        }
       
        [HttpGet]
        [Route("app/redirect", Name = "Redirect")]
        [SafeActionAttribute]
        public ActionResult Redirect()
        {
            return View();
        }
        [Route("app/forgotpassword", Name = "AppForgotPassword")]
        [SafeActionAttribute]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("app/resetpassword", Name = "AppResetpassword")]
        [SafeActionAttribute]
        public ActionResult Resetpassword()
        {
            return View();
        }

    }
}
