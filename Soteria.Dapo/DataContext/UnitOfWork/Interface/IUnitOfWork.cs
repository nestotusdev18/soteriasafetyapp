using Soteria.DataComponents.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository GenericRepository { get; }
        void Commit();
    }
}
