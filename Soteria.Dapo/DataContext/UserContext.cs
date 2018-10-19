using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.DataContext
{
    public static class UserContext
    {
        public static ApplicationUser Authenticate(AuthenticationRequest authenticationRequest)
        {
            using (var uow = new UnitOfWork())
            {
                authenticationRequest.Password = Cryptography.Encrypt(authenticationRequest.Password);
                var ret = uow.UserRepository.Authenticate(authenticationRequest);
                uow.Commit();
                return ret;
            }
        }

        public static AppConfig GetAppConfig(AuthorizationToken token)
        {
            using (var uow = new UnitOfWork())
            {
                var ret = uow.UserRepository.GetAppConfig(token);
                uow.Commit();
                return ret;
            }
        }
    }
}
