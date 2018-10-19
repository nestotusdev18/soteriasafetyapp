using Soteria.DataComponents.DataModel;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.DataContext
{
    public static class MasterContext
    {
        public static SchoolTable GetSchool(AuthorizationToken token)
        {
            using (var uow = new UnitOfWork())
            {
                var ret = uow.GenericRepository.Find<SchoolTable>(token.SchoolID);
                uow.Commit();
                return ret;
            }
        }
    }
}
