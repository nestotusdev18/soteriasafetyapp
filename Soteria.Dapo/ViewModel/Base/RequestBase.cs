using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Base
{
    public class RequestBase
    {
        public string TokenID { get; set; }
    }

    public class AuthorizationToken
    {
        public long UserID { get; set; }
        public long SchoolID { get; set; }
        public long SchoolDistrictID { get; set; }
        public long RoleID { get; set; }
    }
}
