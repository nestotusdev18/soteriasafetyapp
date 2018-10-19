using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Gateway
{
    public class GatewayLogPayload
    {
        public string timestamp { get; set; }
        public string type { get; set; }
        public string mac { get; set; }
        public string bleName { get; set; }
        public string ibeaconUuid { get; set; }
        public int ibeaconMajor { get; set; }
        public int ibeaconMinor { get; set; }
        public int ibeaconTxPower { get; set; }
        public int rssi { get; set; }
        public int battery { get; set; }
        public double? temperature { get; set; }
        public double? humidity { get; set; }
        public string rawData { get; set; }
    }
}
