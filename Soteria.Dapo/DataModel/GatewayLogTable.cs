using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.DataModel
{
    [Table("[dbo].[Gateway.Log]")]
    public class GatewayLogTable : DbObjectBase
    {
        [Key]
        public int GatewayLogID { get; set; }
        public DateTime? LogDateTime { get; set; }
        public string BeaconType { get; set; }
        public string Mac { get; set; }
        public string UUID { get; set; }
        public int MajorID { get; set; }
        public int MinorID { get; set; }
        public decimal TxPower { get; set; }
        public decimal rssi { get; set; }
        public Guid? BatchID { get; set; }
        public string GatewayMac { get; set; }
    }

    [Table("[dbo].[Gateway.JsonLog]")]
    public class GatewayJsonLogTable : DbObjectBase
    {
        [Key]
        public int GatewayJsonLogID { get; set; }
        public string InboundLog { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
