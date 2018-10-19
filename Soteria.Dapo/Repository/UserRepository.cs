using Soteria.DataComponents.Repository.Interface;
using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Soteria.DataComponents.ViewModel.Base;

namespace Soteria.DataComponents.Repository
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public ApplicationUser Authenticate(AuthenticationRequest authenticationRequest)
        {
            return Connection.QueryFirstOrDefault<ApplicationUser>("[AuthenticateUser]",
                new
                {
                    UserName = authenticationRequest.UserName,
                    Password = authenticationRequest.Password,
                    IPAddress = authenticationRequest.IPAddress,
                    LoginCode = authenticationRequest.LoginCode
                },
                commandType: CommandType.StoredProcedure,
                transaction: Transaction);
        }

        public AppConfig GetAppConfig(AuthorizationToken token)
        {
            return Connection.QueryFirstOrDefault<AppConfig>("[GetAppConfig]",
                new
                {
                    SchoolDistrictID = token.SchoolDistrictID,
                    SchoolID = token.SchoolID,
                    UserID = token.UserID
                },
                commandType: CommandType.StoredProcedure,
                transaction: Transaction);
        }
    }
}
