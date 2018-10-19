using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.ViewModel.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Repository.Interface
{
    public interface ISRORepository
    {
        CheckpointBeaconActivityMaster GetCheckpointBeaconActivityMaster(AuthorizationToken token);
    }
}
