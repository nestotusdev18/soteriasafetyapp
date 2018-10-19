using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Common
{
    public class SearchCriteria
    {
        public int FacilityId { get; set; }
        public long CustomerId { get; set; }
        public DateTime? StartDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? EndDate { get; set; }
        public long UserId { get; set; }
        public int VendorId { get; set; }
        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
    }
}
