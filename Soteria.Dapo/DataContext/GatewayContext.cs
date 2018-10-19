using Soteria.DataComponents.DataModel;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.DataComponents.Infrastructure.Common;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.ViewModel.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Soteria.DataComponents.DataContext
{
    public static class GatewayContext
    {
        public static async Task LogGatewayPayload(List<GatewayLogPayload> gatewayLogPayload, RoomType roomType)
        {
            try
            {
                if (gatewayLogPayload == null)
                {
                    ExceptionLogger.LogExceptionAsync(new Exception("Payload empty"), 1, 1, Infrastructure.Enum.ExceptionSeverity.Exception);
                    return;
                }

                //log the inbound json string
                string json = Helper.ObjectToJson(gatewayLogPayload);
                using (var uow = new UnitOfWork())
                {
                    await uow.GenericRepository.AddAsync<GatewayJsonLogTable>(new GatewayJsonLogTable()
                    {
                        InboundLog = json,
                        DateCreated = DateTime.UtcNow
                    });
                    uow.Commit();
                }

                //log individual beacons
                var gateWayInfo = gatewayLogPayload.Where(x => x.type == "Gateway").FirstOrDefault();
                string gatewayMac = "";
                if (gateWayInfo != null)
                    gatewayMac = gateWayInfo.mac;
                Guid guid = Guid.NewGuid();

                var datatable = GetGatewayLogTable();
                foreach (var payload in gatewayLogPayload)
                {
                    var row = datatable.NewRow();
                    row["LogDateTime"] = Convert.ToDateTime(payload.timestamp);
                    row["BeaconType"] = payload.type;
                    row["Mac"] = payload.mac;
                    row["UUID"] = payload.ibeaconUuid;
                    row["MajorID"] = payload.ibeaconMajor;
                    row["MinorID"] = payload.ibeaconMinor;
                    row["TxPower"] = payload.ibeaconTxPower;
                    row["rssi"] = payload.rssi;
                    row["BatchID"] = guid;
                    row["GatewayMac"] = gatewayMac;
                    datatable.Rows.Add(row);
                }

                using (var uow = new UnitOfWork())
                {
                    /*foreach (var data in gatewayLogPayload)
                    {
                        var logdate = Convert.ToDateTime(data.timestamp);
                        DateTime.SpecifyKind(logdate, DateTimeKind.Unspecified);
                        await uow.GenericRepository.AddAsync<GatewayLogTable>(new GatewayLogTable()
                        {
                            BeaconType = data.type,
                            LogDateTime = Convert.ToDateTime(data.timestamp),
                            Mac = data.mac,
                            UUID = data.ibeaconUuid,
                            MajorID = data.ibeaconMajor,
                            MinorID = data.ibeaconMinor,
                            TxPower = data.ibeaconTxPower,
                            rssi = data.rssi,
                            GatewayMac = gatewayMac,
                            BatchID = guid
                        });
                    }
                    uow.Commit();*/

                    //log individual beacons
                    await uow.GatewayRepository.AddGatewayLog(datatable);

                    //parse and get counts
                    await uow.GenericRepository.ExecAsync("EXEC [dbo].[ExecuteBatchSummaryReport] @BatchID", new
                    {
                        BatchID = guid
                    });
                    uow.Commit();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExceptionAsync(ex, 1, 1, Infrastructure.Enum.ExceptionSeverity.Exception);
            }
            finally
            {

            }
        }

        private static DataTable GetGatewayLogTable()
        {
            var datatable = new DataTable();
            datatable.Columns.Add("LogDateTime");
            datatable.Columns.Add("BeaconType");
            datatable.Columns.Add("Mac");
            datatable.Columns.Add("UUID");
            datatable.Columns.Add("MajorID");
            datatable.Columns.Add("MinorID");
            datatable.Columns.Add("TxPower");
            datatable.Columns.Add("rssi");
            datatable.Columns.Add("BatchID");
            datatable.Columns.Add("GatewayMac");
            return datatable;
        }
    }
}
