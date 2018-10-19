using System.ComponentModel.DataAnnotations;

namespace Soteria.DataComponents.ViewModel
{
    public class RecordRequestPayload
    {
        [Required]
        public long RecordId { get; set; }
    }
    public class RecordCreateResponse : BaseResponse
    {
        public long? RecordId { get; set; }
    }

    public class RecordUpdateResponse : BaseResponse
    {
        public bool? IsRecordUpdated { get; set; }
    }

    public class RecordCheckResponse : BaseResponse
    {
        public bool? IsRecordExists { get; set; }
    }
}
