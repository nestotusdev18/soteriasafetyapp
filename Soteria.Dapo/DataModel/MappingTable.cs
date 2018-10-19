using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Soteria.DataComponents.DataModel
{
    [Table("[Mapping.DocumentActivity]")]
    public class DocumentActivityMappingTable : DbObjectBase
    {
        [Key]
        public long DocumentActivityMappingID { get; set; }
        public long DocumentID { get; set; }
        public long? ActivityLogID { get; set; }
        public string ActivityType { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    [Table("[Mapping.Note]")]
    public class NoteMappingTable : DbObjectBase
    {
        [Key]
        public long MappingId { get; set; }
        public long NoteId { get; set; }
        public string Mapping { get; set; }
        public long AssociationId { get; set; }
    }

    [Table("[Persona.Note]")]
    public class NoteTable : DbObjectBase
    {
        [Key]
        public long NoteId { get; set; }
        public long UserId { get; set; }
        public string Comments { get; set; }
        public DateTime? Timestamp { get; set; }
    }


}
