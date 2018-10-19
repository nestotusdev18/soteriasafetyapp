using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Mobile
{
    public class CheckpointActivity
    {
        public long ActivityID { get; set; }
        public string ActivityName { get; set; }
    }

    public class Checkpoint
    {
        public long CheckpointID { get; set; }
        public string CheckpointName { get; set; }
        public List<CheckpointActivity> CheckpointActivities { get; set; }
    }

    public class CheckpointBeacon
    {
        public CheckpointBeacon()
        {
            this.Checkpoints = new List<Checkpoint>();
        }

        public long CheckpointBeaconID { get; set; }
        public string BeaconName { get; set; }
        public int? MajorID { get; set; }
        public int? MinorID { get; set; }
        public List<Checkpoint> Checkpoints { get; set; }
    }

    public class CheckpointBeaconActivityMaster
    {
        public List<CheckpointBeacon> CheckpointBeacons { get; set; }
    }

    public class CheckpointBeaconActivityMasterResponse : BaseResponse
    {
        public CheckpointBeaconActivityMaster CheckpointBeaconActivityMaster { get; set; }
    }

    public class CheckpointActivityLogRequest
    {
        public List<int> CheckpointID { get; set; }
        public List<int> ActivityID { get; set; }
        public string Notes { get; set; }
        public DateTime? LogDateTime { get; set; }
        public List<FileUpload> FileNames { get; set; }
        public string OfflineTransactionID { get; set; }
    }
}
