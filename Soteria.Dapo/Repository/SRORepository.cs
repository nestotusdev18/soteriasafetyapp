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

namespace Soteria.DataComponents.Repository
{
    internal class SRORepository : RepositoryBase, ISRORepository
    {
        public SRORepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public CheckpointBeaconActivityMaster GetCheckpointBeaconActivityMaster(AuthorizationToken token)
        {
            var multiResult = Connection.QueryMultiple("GetCheckpointBeaconMappingAndActivityMaster", new
            {
                SchoolID = token.SchoolID,
                UserID = token.UserID
            },
            commandType: CommandType.StoredProcedure,
            transaction: Transaction);

            var cpbeaconsTableList = multiResult.Read<MasterCheckpointBeaconTable>().ToList();
            var checkpointsTableList = multiResult.Read<MasterCheckpointTable>().ToList();
            var cpbeaconMapTableList = multiResult.Read<CheckpointBeaconMappingTable>().ToList();
            var cpActivityMapTableList = multiResult.Read<CheckpointActivityMapping>().ToList();


            List<CheckpointBeacon> checkpointBeacons = new List<CheckpointBeacon>();
            foreach (MasterCheckpointBeaconTable mastercheckpointbeaconRow in cpbeaconsTableList)
            {
                var mappedCheckPoints = cpbeaconMapTableList.Where(x => x.CheckpointBeaconID == mastercheckpointbeaconRow.CheckpointBeaconID).ToList();

                List<Checkpoint> checkpoints = new List<Checkpoint>();
                foreach (CheckpointBeaconMappingTable checkpointBeaconMappingRow in mappedCheckPoints)
                {
                    var cpRow = checkpointsTableList.Where(x => x.CheckpointID == checkpointBeaconMappingRow.CheckpointID).FirstOrDefault();
                    if (cpRow != null)
                    {

                        var activityTable = cpActivityMapTableList.Where(x => x.CheckpointID == cpRow.CheckpointID).ToList();

                        List<CheckpointActivity> activityList = new List<CheckpointActivity>();
                        foreach (CheckpointActivityMapping activityRow in activityTable)
                        {
                            var activity = new CheckpointActivity()
                            {
                                ActivityID = activityRow.ActivityID,
                                ActivityName = activityRow.ActivityType
                            };
                            activityList.Add(activity);
                        }

                        var checkpoint = new Checkpoint()
                        {
                            CheckpointID = cpRow.CheckpointID,
                            CheckpointName = cpRow.CheckpointName,
                            CheckpointActivities = activityList
                        };
                        checkpoints.Add(checkpoint);
                    }
                }

                var checkpointbeacon = new CheckpointBeacon()
                {
                    CheckpointBeaconID = mastercheckpointbeaconRow.CheckpointBeaconID,
                    BeaconName = mastercheckpointbeaconRow.BeaconName,
                    MajorID = mastercheckpointbeaconRow.MajorID,
                    MinorID = mastercheckpointbeaconRow.MinorID,
                    Checkpoints = checkpoints
                };

                checkpointBeacons.Add(checkpointbeacon);
            }

            CheckpointBeaconActivityMaster checkpointBeaconActivityMaster = new CheckpointBeaconActivityMaster();
            checkpointBeaconActivityMaster.CheckpointBeacons = checkpointBeacons;
            return checkpointBeaconActivityMaster;
        }
    }
}
