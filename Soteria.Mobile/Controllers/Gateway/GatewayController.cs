using Soteria.DataComponents;
using Soteria.DataComponents.DataContext;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.Infrastructure.Common;
using Soteria.DataComponents.ViewModel;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.ViewModel.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using Soteria.DataComponents.Infrastructure.Enum;
using System.Threading.Tasks;

namespace Soteria.Mobile.Controllers
{
    [RoutePrefix("gateway/api/log")]
    public class GatewayController : ApiController
    {
        [Route("boysrestroom")]
        [HttpPost]
        public async Task LogBoysRestRoom(List<GatewayLogPayload> gatewayLogPayload)
        {
            await GatewayContext.LogGatewayPayload(gatewayLogPayload, RoomType.RestRoomBoys);
        }

        [Route("girlsrestroom")]
        [HttpPost]
        public void LogGirlsRestRoom(List<GatewayLogPayload> gatewayLogPayload)
        {
            //GatewayContext.LogGatewayPayload(gatewayLogPayload, RoomType.RestRoomGirls);
        }
    }
}
