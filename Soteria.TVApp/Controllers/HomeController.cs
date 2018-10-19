using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Soteria.TVApp.Controllers.View;
using Soteria.TVApp.Models;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.DataContext;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.DataComponents.ViewModel.Auth;
using Soteria.TVApp.Filter;
using Soteria.TVApp.Auth;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Soteria.TVApp.Controllers
{
    public class HomeController :  BaseViewController
    {
        public List<BathroomSummaryLog> DataObject { get; private set; }

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
                ViewBag.Title = userSession.SchoolName;
                ViewBag.HeaderToken = Session["HeaderToken"];
                ViewBag.UserName = Session["UserName"];
                ViewBag.UserID = userSession.UserID;
                ViewBag.SchoolID = userSession.SchoolID;
                ViewBag.SchoolCode = userSession.SchoolCode;
                ViewBag.UserSessionData = userSession;
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
        [Route("app/login")]
        [Route("~/")]
        [SafeActionAttribute]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Route("app/logout")]
        public ActionResult Logout()
        {
            FormAuthManager.LogoutNow();
            return RedirectoSafeRoute("AppLogin");
        }

        [HttpPost]
        [Route("app/login", Name = "AppLogin")]
        public ActionResult Login(AuthenticationRequest authenticationRequest)
        {
            ApplicationUser applicationUser = UserContext.Authenticate(authenticationRequest);

            if (applicationUser != null)
            {
                /*Debug.WriteLine("After Login");

                var TokenID = JwtManager.GenerateToken(new SessionToken()
                {
                    RoleID = applicationUser.RoleID,
                    UserID = applicationUser.UserID,
                    SchoolDistrictID = applicationUser.SchoolDistrictID,
                    SchoolID = applicationUser.SchoolID
                });
                Session["HeaderToken"] = TokenID;
                 FormAuthManager.AuthenticateNow(new SessionToken()
                {
                    RoleID = applicationUser.RoleID,
                    UserID = applicationUser.UserID,
                    SchoolDistrictID = applicationUser.SchoolDistrictID,
                    SchoolID = applicationUser.SchoolID
                });
                */

                Session["UserSession"] = applicationUser;
                Session["SchoolID"] = applicationUser.SchoolID;
                Session["UserName"] = applicationUser.FirstName;
                ViewBag.Title = applicationUser.SchoolName;
                //return RedirectoSafeRoute("AppDashboard");
                return RedirectoSafeRoute("AppHome");
            }
            else
            {
                ModelState.AddModelError("loginsummary", "User Authentication Fails, Username and/or LoginCode and/or Password doesn't work");
            }

            return View(authenticationRequest);

        }

        public JsonResult Datacount()
        {
            var searchCriteria = new SearchCriteria();
            ApplicationUser userSession = (ApplicationUser)Session["UserSession"];
            searchCriteria.SchoolId = (int)userSession.SchoolID;
            DataObject = StudentContext.GetBathroomSummaryLog(searchCriteria);
            return Json(DataObject, JsonRequestBehavior.AllowGet);
        }


        [Route("AppDashboard", Name = "AppDashboard")]
        [SafeActionAttribute]
        public ActionResult Dashboard()
        {
            ApplicationUser userSession = (ApplicationUser)Session["UserSession"];
            if (userSession == null)
            {
                return RedirectoSafeRoute("AppLogin");
            }

            ViewBag.Title = userSession.SchoolName;
            ViewBag.HeaderToken = Session["HeaderToken"];
            ViewBag.UserName = Session["UserName"];
            ViewBag.UserID = userSession.UserID;
            ViewBag.SchoolID = userSession.SchoolID;
            ViewBag.SchoolCode = userSession.SchoolCode;
            ViewBag.SchoolName = userSession.SchoolName;
            return View();
        }


        


    }
}
