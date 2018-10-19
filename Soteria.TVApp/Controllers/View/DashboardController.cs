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
            ViewBag.UserId = searchCriteria.UserId;
            ViewBag.SchoolId = searchCriteria.SchoolId;
            ViewBag.SchoolCode = searchCriteria.SchoolCode;
            ApplicationUser userSession = (ApplicationUser)Session["UserSession"];
            RoleType RoleName = (RoleType)Enum.Parse(typeof(RoleType), userSession.RoleName);
            ViewBag.RoleName = RoleName;
            return PartialView("Dashboard");
        }



    }
}