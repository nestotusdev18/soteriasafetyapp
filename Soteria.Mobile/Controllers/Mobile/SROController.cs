using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Soteria.DataComponents;
using Soteria.DataComponents.DataContext;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.Infrastructure.Common;
using Soteria.DataComponents.ViewModel;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.ViewModel.Mobile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.Mobile.Filter;

namespace Soteria.Mobile.Controllers
{
    [RoutePrefix("mobile/api/sro")]
    public class SROController : BaseApiController
    {
        [Route("checkpointbeaconactivitymaster")]
        [HttpGet]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public CheckpointBeaconActivityMasterResponse GetCheckpointBeaconActivityMaster()
        {
            //getbeaconcheckpointmapping
            //getmastercheckpointactivity - by activity id in checkpoint table
            //savecheckpointactivity

            try
            {
                var token = GetAuthorizationToken();

                var response = SROContext.GetCheckpointBeaconActivityMaster(token);

                return new CheckpointBeaconActivityMasterResponse()
                {
                    CheckpointBeaconActivityMaster = response
                };
                

                var activityList1 = new List<CheckpointActivity>();
                activityList1.Add(new CheckpointActivity() { ActivityID = 1, ActivityName = "Door secured" });
                activityList1.Add(new CheckpointActivity() { ActivityID = 2, ActivityName = "Door Opened" });

                var activityList2 = new List<CheckpointActivity>();
                activityList2.Add(new CheckpointActivity() { ActivityID = 3, ActivityName = "Alarm Enabled" });
                activityList2.Add(new CheckpointActivity() { ActivityID = 4, ActivityName = "Alarm Disabled" });

                var activityList3 = new List<CheckpointActivity>();
                activityList3.Add(new CheckpointActivity() { ActivityID = 5, ActivityName = "West wing hallway security check" });

                var activityList4 = new List<CheckpointActivity>();
                activityList4.Add(new CheckpointActivity() { ActivityID = 6, ActivityName = "East wing hallway security check" });

                var checkpoint1 = new Checkpoint() { CheckpointID = 1, CheckpointName = "Front Door", CheckpointActivities = activityList1 };
                var checkpoint2 = new Checkpoint() { CheckpointID = 2, CheckpointName = "Side Door Alarm", CheckpointActivities = activityList2 };
                var checkpoint3 = new Checkpoint() { CheckpointID = 3, CheckpointName = "West Hallway", CheckpointActivities = activityList3 };
                var checkpoint4 = new Checkpoint() { CheckpointID = 4, CheckpointName = "East Hallway", CheckpointActivities = activityList4 };

                var checkpointList1 = new List<Checkpoint>();
                checkpointList1.Add(checkpoint1);

                var checkpointList2 = new List<Checkpoint>();
                checkpointList2.Add(checkpoint2);

                var checkpointList3 = new List<Checkpoint>();
                checkpointList3.Add(checkpoint3);

                var checkpointList4 = new List<Checkpoint>();
                checkpointList4.Add(checkpoint4);

                var checkpointbeacon1 = new CheckpointBeacon()
                {
                    CheckpointBeaconID = 1, BeaconName = "Front Door Beacon", MajorID = 10, MinorID = 1, Checkpoints = checkpointList1
                };

                var checkpointbeacon2 = new CheckpointBeacon()
                {
                    CheckpointBeaconID = 2,
                    BeaconName = "Side Door Alarm Beacon",
                    MajorID = 10,
                    MinorID = 2,
                    Checkpoints = checkpointList2
                };

                var checkpointbeacon3 = new CheckpointBeacon()
                {
                    CheckpointBeaconID = 3,
                    BeaconName = "West Hallway Beacon",
                    MajorID = 10,
                    MinorID = 3,
                    Checkpoints = checkpointList3
                };

                var checkpointbeacon4 = new CheckpointBeacon()
                {
                    CheckpointBeaconID = 4,
                    BeaconName = "West Hallway Beacon",
                    MajorID = 10,
                    MinorID = 4,
                    Checkpoints = checkpointList4
                };

                var list = new List<CheckpointBeacon>();
                list.Add(checkpointbeacon1);
                list.Add(checkpointbeacon2);
                list.Add(checkpointbeacon3);
                list.Add(checkpointbeacon4);

                var checkpointBeaconActivityMaster = new CheckpointBeaconActivityMaster()
                {
                    CheckpointBeacons = list
                };

                return new CheckpointBeaconActivityMasterResponse()
                {
                    CheckpointBeaconActivityMaster = checkpointBeaconActivityMaster
                };
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new CheckpointBeaconActivityMasterResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }
        }

        [Route("checkpointactivitylog")]
        [HttpPost]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public BaseResponse CheckpointaActivityLog(CheckpointActivityLogRequest checkpointActivityLogRequest)
        {
            try
            {
                if (checkpointActivityLogRequest.CheckpointID == null || checkpointActivityLogRequest.CheckpointID.Count == 0)
                    throw new Exception("Invalid payload");

                SROContext.PostCheckpointActivityLog(GetAuthorizationToken(), checkpointActivityLogRequest);
                return new BaseResponse();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new BaseResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }
        }
    }
}
