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
    public static class StudentContext
    {
        public static List<MasterActivityStudent> GetMasterActivityStudent(AuthorizationToken token)
        {
            using (var uow = new UnitOfWork())
            {
                var ret = uow.StudentRepository.GetMasterActivityStudent(token);
                uow.Commit();
                return ret;
            }
        }

        public static List<Student> GetStudentDetails(AuthorizationToken token, StudentSearchRequest studentSearchRequest)
        {
            using (var uow = new UnitOfWork())
            {
                var ret = uow.StudentRepository.GetStudentDetails(token, studentSearchRequest);
                uow.Commit();
                return ret;
            }
        }

        public static string AssignIdToStudent(AuthorizationToken token, StudentIDCardMapping studentIDCardMapping)
        {
            using (var uow = new UnitOfWork())
            {
                var msg = uow.StudentRepository.AssignIdToStudent(token, studentIDCardMapping);
                uow.Commit();
                return msg;
            }
        }

        public static List<IDCardAvailabilityCheck> CheckIDCardAvailability(AuthorizationToken token, IDCardAvailabilityCheckRequestPayload IDCardAvailabilityCheckRequestPayload)
        {
            var list = new List<IDCardAvailabilityCheck>();
            using (var uow = new UnitOfWork())
            {
                foreach (var payload in IDCardAvailabilityCheckRequestPayload.IDCardAvailabilityCheckRequest)
                {
                    var check = uow.StudentRepository.CheckIDCardAvailability(token, payload);
                    list.Add(check);
                    //uow.Commit();
                }
            }
            return list;
        }

        public static void PostStudentInteraction(AuthorizationToken token, StudentInteractionRequest studentInteractionRequest)
        {
            /*
             * 
             * 1. Add student interaction log, get activitylogid
             * 2. Add notes, get id and add note mapping
             * 3. Add document, get id and add document activity mapping
             */

            //var studentInteractionPayload = new StudentInteractionPayload();

            if (studentInteractionRequest.StudentID == null || studentInteractionRequest.StudentID.Count == 0)
                return;

            var school = MasterContext.GetSchool(token);

            using (var uow = new UnitOfWork())
            {
                var timeZone = DateTimeConverter.GetFacilityTimeZone(school.SchoolTimeZone);
                bool hasPhoto = (studentInteractionRequest.FileNames != null && studentInteractionRequest.FileNames.Count > 0) ? true : false;
                bool hasNotes = (studentInteractionRequest.Notes.Trim().Length > 0) ? true : false;
                var logdatetime = DateTimeConverter.ConvertDateTimeToUTC(studentInteractionRequest.LogDateTime.GetValueOrDefault(), timeZone);

                var incidentGroupID = Guid.NewGuid();

                foreach (var _studentid in studentInteractionRequest.StudentID)
                {
                    foreach (var _activityId in studentInteractionRequest.ActivityID)
                    {
                        //add activity
                        var activityLogId = uow.GenericRepository.Add<StudentInteractionTable>(new StudentInteractionTable()
                        {
                            StudentID = _studentid,
                            ActivityID = _activityId,
                            SchoolID = token.SchoolID,
                            CreatedBy = token.UserID,
                            DateCreated = DateTime.UtcNow,
                            LogDatetime = logdatetime,
                            HasNotes = hasNotes,
                            HasPhoto = hasPhoto,
                            IncidentGroupID = incidentGroupID
                        });

                        //add note with activityId generated above
                        var noteId = uow.GenericRepository.Add<NoteTable>(new NoteTable()
                        {
                            Comments = studentInteractionRequest.Notes,
                            UserId = token.UserID,
                            Timestamp = logdatetime
                        });

                        //add note mapping with note id and activityId generated above
                        uow.GenericRepository.Add<NoteMappingTable>(new NoteMappingTable()
                        {
                            NoteId = noteId,
                            AssociationId = activityLogId,
                            Mapping = "StudentInteraction"
                        });

                        //for each activity added add the attached(image) and its mapping

                        if (studentInteractionRequest.FileNames != null)
                        {
                            foreach (var image in studentInteractionRequest.FileNames)
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
                                    ActivityType = "StudentInteraction"
                                });
                            }
                        }
                    }
                }
                uow.Commit();
            }
        }

        public static List<BathroomSummaryLog> GetBathroomSummaryLog(SearchCriteria SearchCriteria)
        {
            using (var uow = new UnitOfWork())
            {
                var ret = uow.StudentRepository.GetBathroomSummaryLog(SearchCriteria);
                uow.Commit();
                return ret;
            }
        }
    }
}
