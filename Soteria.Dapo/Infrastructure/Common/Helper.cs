using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Infrastructure.Common
{
    public class Helper
    {
        public static string GenerateToken(ApplicationUser applicationUser)
        {
            StringBuilder tokenID = new StringBuilder();
            tokenID.Append(Utility.NullToString(applicationUser.UserID));
            tokenID.Append(",");
            tokenID.Append(Utility.NullToString(applicationUser.SchoolID));
            tokenID.Append(",");
            tokenID.Append(Utility.NullToString(applicationUser.SchoolDistrictID));
            tokenID.Append(",");
            tokenID.Append(Utility.NullToString(applicationUser.RoleID));
            tokenID.Append(",");
            tokenID.Append(DateTime.UtcNow);
            return Cryptography.Encrypt(tokenID.ToString());
        }

        public static string ObjectToJson(object o)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(o);
            return json;
        }
    }
}
