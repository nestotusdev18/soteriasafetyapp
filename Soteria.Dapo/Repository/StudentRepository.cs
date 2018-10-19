using Soteria.DataComponents.Repository.Interface;
using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Mobile;

namespace Soteria.DataComponents.Repository
{
    internal class StudentRepository : RepositoryBase, IStudentRepository
    {
        public StudentRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public List<MasterActivityStudent> GetMasterActivityStudent(AuthorizationToken token)
        {
            return Connection.Query<MasterActivityStudent>("[GetStudentMasterActivities]",
                new
                {
                    SchoolID = token.SchoolID,
                    UserID = token.UserID
                },
                commandType: CommandType.StoredProcedure,
                transaction: Transaction).AsList();
        }

        public List<Student> GetStudentDetails(AuthorizationToken token, StudentSearchRequest studentSearchRequest)
        {
            var idList = new DataTable();
            idList.Columns.Add("ID");
            if (studentSearchRequest.MinorID != null)
            {
                foreach (int id in studentSearchRequest.MinorID)
                {
                    var dr = idList.NewRow();
                    dr["ID"] = id;
                    idList.Rows.Add(dr);
                }
            }
            return Connection.Query<Student>("[GetStudentDetails]",
                 new
                 {
                     SchoolID = token.SchoolID,
                     SchoolDisrictID = token.SchoolDistrictID,
                     UserID = token.UserID,
                     MinorIDs = idList.AsTableValuedParameter("Uddt_IDList"),
                     Name = studentSearchRequest.Name,
                     Grade = studentSearchRequest.Grade,
                     StudentSchoolID = studentSearchRequest.StudentSchoolID
                 },
                 commandType: CommandType.StoredProcedure,
                 transaction: Transaction).AsList();
        }

        public void AddStudentInteraction(AuthorizationToken token, StudentInteractionRequest studentInteractionRequest)
        {
            throw new Exception("handled in data context");
        }

        public string AssignIdToStudent(AuthorizationToken token, StudentIDCardMapping studentIDCardMapping)
        {
            var success = Connection.ExecuteScalar("[AssignStudentIdCard]",
                 new
                 {
                     SchoolID = token.SchoolID,
                     UserID = token.UserID,
                     StudentID = studentIDCardMapping.StudentID,
                     MajorID = studentIDCardMapping.MajorID,
                     MinorID = studentIDCardMapping.MinorID
                 },
                 commandType: CommandType.StoredProcedure,
                 transaction: Transaction);

            return Convert.ToString(success);
        }

        public IDCardAvailabilityCheck CheckIDCardAvailability(AuthorizationToken token, IDCardAvailabilityCheckRequest idCardAvailabilityCheckRequest)
        {
            return Connection.QueryFirstOrDefault<IDCardAvailabilityCheck>("[IDCardAvailabilityCheck]", new
            {
                SchoolID = token.SchoolID,
                UserID = token.UserID,
                MajorID = idCardAvailabilityCheckRequest.MajorID,
                MinorID = idCardAvailabilityCheckRequest.MinorID,
                Mac = idCardAvailabilityCheckRequest.Mac
            }, commandType: CommandType.StoredProcedure,
            transaction: Transaction);
        }


        public List<BathroomSummaryLog> GetBathroomSummaryLog(SearchCriteria SearchCriteria)
        {
            return Connection.Query<BathroomSummaryLog>("[GetBathroomSummaryLog]",
                new
                {
                    SchoolID = SearchCriteria.SchoolId,
                },
                commandType: CommandType.StoredProcedure,
                transaction: Transaction).AsList();
        }
    }
}
