using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Infrastructure.Enum
{
    public enum ExceptionSeverity
    {
        Severity = 1,
        Exception = 2,
        Warning = 3,
        Logging = 4
    }

    public enum RoomType
    {
        ClassRoom = 1,
        RestRoomBoys = 2,
        RestRoomGirls = 3
    }

    public enum RoleType
    {
        Anonymous = 0,
        SuperAdmin = 1,
        SchoolAdmin = 2,
        SRO = 3
    }
}
