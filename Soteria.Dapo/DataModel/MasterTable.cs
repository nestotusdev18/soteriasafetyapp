using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Soteria.DataComponents.DataModel
{
    [Table("[App.School]")]
    public class SchoolTable : DbObjectBase
    {
        [Key]
        public long SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public long SchoolDistrictID { get; set; }
        public string EMailID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int? StateID { get; set; }
        public string ZIP { get; set; }
        public string SchoolTimeZone { get; set; }
        public Guid? UUID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string StorageUrl { get; set; }
    }

    [Table("Document")]
    public class DocumentTable : DbObjectBase
    {
        [Key]
        public long DocumentID { get; set; }
        public long? SchoolID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }

}
