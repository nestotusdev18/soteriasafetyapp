using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Common
{
    public class MasterActivityStudent
    {
        public int ActivityID { get; set; }
        public string ActivityType { get; set; }
    }

    public class MasterActivityStudentResponse : BaseResponse
    {
        public List<MasterActivityStudent> MasterActivtiesStudent { get; set; }
    }

    public class BathroomSummaryLog
    {
        public string FloorName { get; set; }
        public int UniqueId { get; set; }
        public int SchoolId { get; set; }
        public int RoomType { get; set; }
        public bool IsEmergency { get; set; }
        public int CurrentCount { get; set; }
        public int LongStay { get; set; }
        public int LongRecent { get; set; }
        public int WrongPerson { get; set; }
        public int WrongPersonRecent { get; set; }
        public bool IsOffline { get; set; }
    }

    
}
