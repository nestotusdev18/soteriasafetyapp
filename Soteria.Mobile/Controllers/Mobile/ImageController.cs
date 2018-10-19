using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Soteria.DataComponents;
using Soteria.DataComponents.DataContext;
using Soteria.DataComponents.Infrastructure;
using Soteria.DataComponents.Infrastructure.Common;
using Soteria.DataComponents.ViewModel;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.DataComponents.ViewModel.Mobile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Soteria.DataComponents.Infrastructure.Enum;
using Soteria.Mobile.Filter;

namespace Soteria.Mobile.Controllers
{
    [RoutePrefix("mobile/api/image")]
    public class ImageController : BaseApiController
    {
        [Route("add")]
        [HttpPost]
        public FileUploadResponse AddImage()
        //public async Task<FileUploadResponse> AddImage()
        {
            try
            {
                //var token = GetAuthorizationToken();
                //string souceType = HttpContext.Current.Request.Form["SourceType"];
                var fileUploadResponse = new FileUploadResponse();
                if (Request.Content.IsMimeMultipartContent("form-data"))
                {
                    //var accountName = "tracblue3mobileuploads";// ConfigurationManager.AppSettings["MobileUploadFileShareName"];
                    //var accountKey = "ztUxMPXJbSSqJ3n6zhJ+5Q0sIp5m9/qgnMiYbQnAk+n0bDoESnJnkqxlTBOUffpNV07emyElH/Kau/XF5K82Fw==";

                    var accountName = "soteriafilestorage";
                    var accountKey = "mPNCUQdUcqIHw7+7hYQ26edn7LWRTe6kNbweTUVCr2L56iLIxnYFxRkwR1YMW7T7lkKPKlgR3/0fTatLPXaJWQ==";
                    var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer imagesContainer = blobClient.GetContainerReference("greeneschools");
                    //CloudBlobContainer imagesContainer = blobClient.GetContainerReference("mobileuploads");
                    var provider = new AzureStorageMultipartFormDataStreamProvider(imagesContainer);

                    //await Request.Content.ReadAsMultipartAsync(provider);

                    //var task = Task.Run(async () => { await Request.Content.ReadAsMultipartAsync(provider); });
                    //task.Wait();

                    Task.Run(async () => await Request.Content.ReadAsMultipartAsync(provider)).GetAwaiter().GetResult();

                    foreach (var file in provider.FileData)
                    {
                        string filetype = file.Headers.ContentType.MediaType;
                        filetype = filetype.Replace("image/", String.Empty);
                        var name = new FileUpload()
                        {
                            FileName = file.LocalFileName,
                            Extension = filetype
                        };
                        fileUploadResponse.FileNames.Add(name);
                    }

                }
                return fileUploadResponse;
            }
            catch (Exception ex)
            {
                var errors = new StringBuilder();
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);

                errors.Append(ex.Message);
                var baseex = ex.GetBaseException();
                if (baseex != null)
                    ExceptionLogger.LogException(baseex, -1, 1, ExceptionSeverity.Exception);

                errors.Append(baseex.Message);

                return new FileUploadResponse()
                {
                    OperationResult = GetFailureResult(errors.ToString())
                };
            }
        }

    }
}
