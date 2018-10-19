using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Repository
{
    public abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }
        protected RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
