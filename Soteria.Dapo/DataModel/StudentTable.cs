using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Soteria.DataComponents.DataModel
{
    [Table("[Master.Student]")]
    public class StudentTable : DbObjectBase
    {
        [Key]
        public long StudentID { get; set; }
        public string StudentSchoolID { get; set; }
        public string ThirdPartyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Grade { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string AssignedBathroomType { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoFilePath { get; set; }
    }


    [Table("[Activity.StudentInteraction]")]
    public class StudentInteractionTable : DbObjectBase
    {
        [Key]
        public long StudentInterationID { get; set; }
        public long StudentID { get; set; }
        public long ActivityID { get; set; }
        public long SchoolID { get; set; }
        public DateTime LogDatetime { get; set; }
        public bool HasNotes { get; set; }
        public bool HasPhoto { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid IncidentGroupID { get; set; }
    }
}
