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
    public interface IStudentRepository
    {
        List<MasterActivityStudent> GetMasterActivityStudent(AuthorizationToken token);

        List<Student> GetStudentDetails(AuthorizationToken token, StudentSearchRequest studentSearchRequest);

        void AddStudentInteraction(AuthorizationToken token, StudentInteractionRequest studentInteractionRequest);

        string AssignIdToStudent(AuthorizationToken token, StudentIDCardMapping studentIDCardMapping);

        IDCardAvailabilityCheck CheckIDCardAvailability(AuthorizationToken token, IDCardAvailabilityCheckRequest idCardAvailabilityCheckRequest);

        List<BathroomSummaryLog> GetBathroomSummaryLog(SearchCriteria searchCriteria);
    }
}
