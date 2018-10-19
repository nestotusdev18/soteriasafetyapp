using Soteria.DataComponents.DataModel;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.Infrastructure.Common;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.ViewModel.Mobile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.DataContext
{
    public static class SROContext
    {
        public static CheckpointBeaconActivityMaster GetCheckpointBeaconActivityMaster(AuthorizationToken token)
        {
            using (var uow = new UnitOfWork())
            {
                var data = uow.SRORepository.GetCheckpointBeaconActivityMaster(token);
                uow.Commit();
                return data;
            }
        }

        public static void PostCheckpointActivityLog(AuthorizationToken token, CheckpointActivityLogRequest checkpointActivityLogRequest)
        {
            var school = MasterContext.GetSchool(token);

            using (var uow = new UnitOfWork())
            {
                var timeZone = DateTimeConverter.GetFacilityTimeZone(school.SchoolTimeZone);
                bool hasPhoto = (checkpointActivityLogRequest.FileNames != null && checkpointActivityLogRequest.FileNames.Count > 0) ? true : false;
                bool hasNotes = (checkpointActivityLogRequest.Notes.Trim().Length > 0) ? true : false;
                var logdatetime = DateTimeConverter.ConvertDateTimeToUTC(checkpointActivityLogRequest.LogDateTime.GetValueOrDefault(), timeZone);

                long activityLogId = 0;

                foreach (var _checkpointId in checkpointActivityLogRequest.CheckpointID)
                {
                    foreach (var _activityId in checkpointActivityLogRequest.ActivityID)
                    {
                        //add activity log
                        activityLogId = uow.GenericRepository.Add<CheckpointActivityLogTable>(new CheckpointActivityLogTable()
                        {
                            CheckpointID = _checkpointId,
                            ActivityID = _activityId,
                            SchoolID = token.SchoolID,
                            CreatedBy = token.UserID,
                            CreatedDate = DateTime.UtcNow,
                            LogDateTime = logdatetime,
                            HasNote = hasNotes,
                            HasPhoto = hasPhoto,
                            OfflineTransactionID = checkpointActivityLogRequest.OfflineTransactionID
                        });

                        //add note with activityId generated above
                        var noteId = uow.GenericRepository.Add<NoteTable>(new NoteTable()
                        {
                            Comments = checkpointActivityLogRequest.Notes,
                            UserId = token.UserID,
                            Timestamp = logdatetime
                        });

                        //add note mapping with note id and activityId generated above
                        uow.GenericRepository.Add<NoteMappingTable>(new NoteMappingTable()
                        {
                            NoteId = noteId,
                            AssociationId = activityLogId,
                            Mapping = "CheckpointActivity"
                        });

                        //for each activity added add the attached(image) and its mapping
                        if (checkpointActivityLogRequest.FileNames != null)
                        {
                            foreach (var image in checkpointActivityLogRequest.FileNames)
                            {
                                var document = new DocumentTable();
                                document.DocumentName = image.FileName;
                                document.DocumentType = image.Extension;
                                document.SchoolID = token.SchoolID;
                                document.CreatedDate = logdatetime;
                                //var fullName = string.Format("{0}.{1}", image.FileName, image.Extension);
                                document.DocumentPath = Path.Combine(school.StorageUrl, image.FileName);
                                var documentId = uow.GenericRepository.Add<DocumentTable>(document);

                                var mapId = uow.GenericRepository.Add<DocumentActivityMappingTable>(new DocumentActivityMappingTable()
                                {
                                    ActivityLogID = activityLogId,
                                    DocumentID = documentId,
                                    CreatedDate = logdatetime,
                                    ActivityType = "CheckpointActivity"
                                });
                            }
                        }
                    }

                    // add recent activity log -- will need for alert mechanism
                    var recentLog = uow.GenericRepository.Filter<CheckpointRecentActivityLogTable>(new SearchByCheckpointId()
                    {
                        CheckpointID = _checkpointId
                    }).FirstOrDefault();

                    if (recentLog != null)
                    {
                        recentLog.LogDateTime = logdatetime;
                        recentLog.CheckpointActivityLogID = activityLogId;
                        recentLog.AlertCheckCompleted = false;
                        recentLog.ReminderCheckCompleted = false;
                        recentLog.CreatedDate = DateTime.UtcNow;
                        uow.GenericRepository.Update<CheckpointRecentActivityLogTable>(recentLog);
                    }
                    else
                    {
                        uow.GenericRepository.Add<CheckpointRecentActivityLogTable>(new CheckpointRecentActivityLogTable()
                        {
                            CheckpointID = _checkpointId,
                            CheckpointActivityLogID = activityLogId,
                            LogDateTime = logdatetime,
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }
                uow.Commit();
            }
        }
    }
}
