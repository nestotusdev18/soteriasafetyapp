using Soteria.DataComponents.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Mobile
{
    public class Student
    {
        public long StudentID { get; set; }
        public string StudentSchoolID { get; set; }
        public string ThirdPartyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Grade { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string AssignedBathroomType { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoFilePath { get; set; }
        public string FullName
        {
            get
            {
                return new StringBuilder().Append(FirstName).Append(" ").Append(LastName).ToString();
            }
        }
        public bool HasAssignedIdCard { get; set; }
    }

    public class StudentSearchRequest
    {
        public string Name { get; set; }
        public string Grade { get; set; }
        public string StudentSchoolID { get; set; }
        public List<int> MinorID { get; set; }
    }

    public class StudentDetailsResponse : BaseResponse
    {
        public List<Student> Students { get; set; }
    }

    public class StudentIDCardMapping
    {
        public long StudentID { get; set; }
        public long BeaconID { get; set; }
        public int MajorID { get; set; }
        public int MinorID { get; set; }
    }

    public class StudentInteractionRequest
    {
        public List<int> StudentID { get; set; }
        public List<int> ActivityID { get; set; }
        public string Notes { get; set; }
        public string OfflineTransactionID { get; set; }
        public DateTime? LogDateTime { get; set; }
        public List<FileUpload> FileNames { get; set; }
    }

    //[Obsolete]
    //public class StudentInteractionPayload
    //{
    //    /*public string StudentIDs { get; set; }
    //    public string ActivityIDs { get; set; }
    //    public string Notes { get; set; }
    //    public string LogDateTime { get; set; }*/
    //    public List<StudentInteraction> StudentInteraction { get; set; }
    //    public List<FileUpload> FileNames { get; set; }
    //}

    public class StudentInteraction
    {
        public long StudentID { get; set; }
        public long ActivityID { get; set; }
        public string Notes { get; set; }
        public DateTime? LogDateTime { get; set; }
    }

    public class IDCardAvailabilityCheckRequest
    {
        public int MajorID { get; set; }
        public int MinorID { get; set; }
        public string Mac { get; set; }
    }

    public class IDCardAvailabilityCheckRequestPayload
    {
        public List<IDCardAvailabilityCheckRequest> IDCardAvailabilityCheckRequest { get; set; }
    }

    public class IDCardAvailabilityCheck
    {
        public int MajorID { get; set; }
        public int MinorID { get; set; }
        public string Mac { get; set; }
        public bool IDAssignedToStudent { get; set; }
        public bool IDAvailableInSystem { get; set; }
        /*public long StudentID { get; set; }
        public string StudentSchoolID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }*/
    }

    public class IDCardAvailabilityCheckResponse : BaseResponse
    {
        public List<IDCardAvailabilityCheck> IDCardAvailabilityCheck { get; set; }
    }
}
