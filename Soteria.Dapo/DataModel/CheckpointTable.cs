using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Soteria.DataComponents.DataModel
{
    [Table("[Master.Checkpoint]")]
    public class MasterCheckpointTable : DbObjectBase
    {
        [Key]
        public long CheckpointID { get; set; }
        public string CheckpointCode { get; set; }
        public string CheckpointName { get; set; }
        public long SchoolID { get; set; }
        public long? ActivityGroupID { get; set; }
        public int? SortOrder { get; set; }
    }

    [Table("[Master.CheckpointBeacon]")]
    public class MasterCheckpointBeaconTable : DbObjectBase
    {
        [Key]
        public long CheckpointBeaconID { get; set; }
        public string BeaconName { get; set; }
        public string BeaconDesc { get; set; }
        public long SchoolID { get; set; }
        public string BeaconFloor { get; set; }
        public int? BeaconTypeID { get; set; }
        public int? BeaconBrandID { get; set; }
        public int? MajorID { get; set; }
        public int? MinorID { get; set; }
        public int? ActivityGroupID { get; set; }
        public Guid UUID { get; set; }
    }

    [Table("[Mapping.CheckpointBeacon]")]
    public class CheckpointBeaconMappingTable : DbObjectBase
    {
        [Key]
        public long CheckpointBeaconMappingID { get; set; }
        public long CheckpointID { get; set; }
        public long CheckpointBeaconID { get; set; }
    }

    public class CheckpointActivityMapping
    {
        public long CheckpointID { get; set; }
        public long ActivityID { get; set; }
        public string ActivityType { get; set; }
    }


    [Table("[Activity.CheckpointLog]")]
    public class CheckpointActivityLogTable : DbObjectBase
    {
        [Key]
        public long CheckpointLogID { get; set; }
        public long CheckpointID { get; set; }
        public long? CheckpointBeaconID { get; set; }
        public long ActivityID { get; set; }
        public long SchoolID { get; set; }
        public int? UserID { get; set; }
        public DateTime LogDateTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string OfflineTransactionID { get; set; }
        public bool? AlertCheckCompleted { get; set; }
        public bool? HasNote { get; set; }
        public bool? HasPhoto { get; set; }
        public bool? ReminderCheckCompleted { get; set; }
    }

    [Table("[Activity.CheckpointRecentLog]")]
    public class CheckpointRecentActivityLogTable : DbObjectBase
    {
        [Key]
        public long CheckpointRecentLogID { get; set; }
        public long? CheckpointActivityLogID { get; set; }
        public long CheckpointID { get; set; }
        public DateTime LogDateTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool AlertCheckCompleted { get; set; }
        public bool ReminderCheckCompleted { get; set; }
        public DateTime? LastAlertedDateTime { get; set; }
    }

    public class SearchByCheckpointId
    {
        public long CheckpointID { get; set; }
    }

}
