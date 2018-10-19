using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Auth
{
    public class SessionToken
    {
        public long UserID { get; set; }
        public long SchoolID { get; set; }
        public long SchoolDistrictID { get; set; }
        public long RoleID { get; set; }
    }
    public class UserSession : SessionToken, IPrincipal
    {
        public UserSession(SessionToken sessionToken)
        {
            UserID = sessionToken.UserID;
            SchoolID = sessionToken.SchoolID;
            SchoolDistrictID = sessionToken.SchoolDistrictID;
            RoleID = sessionToken.RoleID;
            this.Identity = new GenericIdentity(Guid.NewGuid().ToString());
        }

        public UserSession(long SessionId)
        {
            this.Identity = new GenericIdentity(SessionId.ToString());
        }

        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }
    }
}
