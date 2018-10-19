using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.DataComponents.ViewModel;
using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soteria.TVApp.Controllers.View
{
    [RoutePrefix("view")]
    public class DashboardController : Controller
    {
        [HttpPost]
        [Route("dashboard")]
        public ActionResult Index([System.Web.Http.FromBody] RecordRequestPayload payload, SearchCriteria searchCriteria)
        {
            ApplicationUser userSession = (ApplicationUser)Session["UserSession"];
            ViewBag.Title = userSession.SchoolName;
            ViewBag.UserName = userSession.UserName;
            ViewBag.UserID = userSession.UserID;
            ViewBag.SchoolID = userSession.SchoolID;
            ViewBag.SchoolCode = userSession.SchoolCode;
            ViewBag.SchoolName = userSession.SchoolName;
            ViewBag.UserSessionData = userSession;
            RoleType RoleName = (RoleType)Enum.Parse(typeof(RoleType), userSession.RoleName);
            ViewBag.RoleName = RoleName;
            return PartialView("Dashboard");
        }

        [HttpPost]
        [Route("checkpointLogActivities")]
        public ActionResult CheckpointLogActivities([System.Web.Http.FromBody] RecordRequestPayload payload, SearchCriteria searchCriteria)
        {

            ApplicationUser userSession = (ApplicationUser)Session["UserSession"];
            ViewBag.Title = userSession.SchoolName;
            ViewBag.UserName = userSession.UserName;
            ViewBag.UserID = userSession.UserID;
            ViewBag.SchoolID = userSession.SchoolID;
            ViewBag.SchoolCode = userSession.SchoolCode;
            ViewBag.SchoolName = userSession.SchoolName;
            ViewBag.UserSessionData = userSession;
            RoleType RoleName = (RoleType)Enum.Parse(typeof(RoleType), userSession.RoleName);
            ViewBag.RoleName = RoleName;
            return PartialView("CheckpointLogActivities");
        }

    }
}