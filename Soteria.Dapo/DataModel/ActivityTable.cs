using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Soteria.DataComponents.DataModel
{
    [Table("Master.Activity")]
    public class ActivityTable : DbObjectBase
    {
        [Key]    
        public long ActivityID { get; set; }
        public string ActivityType { get; set; }
    }
}
