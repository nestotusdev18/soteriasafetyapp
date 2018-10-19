using Soteria.DataComponents.Repository.Interface;
using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Mobile;
using Soteria.DataComponents.DataModel;
using System.Linq;
using System.Threading.Tasks;
using Soteria.DataComponents.ViewModel.Gateway;
using Soteria.DataComponents.Infrastructure.Enum;

namespace Soteria.DataComponents.Repository
{
    internal class GatewayRepository : RepositoryBase, IGatewayRepository
    {
        public GatewayRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task AddGatewayLog(DataTable gatewayLogData)
        {
            await Connection.ExecuteAsync("AddGatewayLog", new
            {
                @GatewayLog = gatewayLogData
            },
            commandType: CommandType.StoredProcedure,
            transaction: Transaction);
        }      
    }
}
