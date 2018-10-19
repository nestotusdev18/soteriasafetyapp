using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Repository.Interface
{
    public interface IUserRepository
    {
        ApplicationUser Authenticate(AuthenticationRequest authenticationRequest);

        AppConfig GetAppConfig(AuthorizationToken token);
    }
}
