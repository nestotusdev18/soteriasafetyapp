using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Common
{
    public class FileUpload
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
    }

    public class FileUploadResponse : BaseResponse
    {
        public FileUploadResponse()
        {
            this.FileNames = new List<FileUpload>();
        }

        public List<FileUpload> FileNames { get; set; }
    }
}
