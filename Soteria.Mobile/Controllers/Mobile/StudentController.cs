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
    [RoutePrefix("mobile/api")]
    public class StudentController : BaseApiController
    {
        [Route("studentdetails")]
        [HttpPost]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public StudentDetailsResponse GetStudentDetails(StudentSearchRequest studentSearchRequest)
        {
            try
            {
                var students = StudentContext.GetStudentDetails(GetAuthorizationToken(), studentSearchRequest);
                return new StudentDetailsResponse()
                {
                    Students = students
                };
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new StudentDetailsResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }
            /*var students = new List<Student>();
            students.Add(new Student() { StudentID = 1, StudentSchoolID = "GCCMS1", FirstName = "Uriah", LastName = "Barnett" });
            students.Add(new Student() { StudentID = 2, StudentSchoolID = "GCCMS2", FirstName = "Zephania", LastName = "Maddox" });
            students.Add(new Student() { StudentID = 3, StudentSchoolID = "GCCMS3", FirstName = "Jameson", LastName = "Higgins" });
            students.Add(new Student() { StudentID = 4, StudentSchoolID = "GCCMS4", FirstName = "Fallon", LastName = "Burns" });

            return new StudentDetailsResponse()
            {
                Students = students
            };*/
        }

        [Route("studentmasteractivities")]
        [HttpGet]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public MasterActivityStudentResponse GetStudentMasterActivities()
        {
            try
            {
                var activites = StudentContext.GetMasterActivityStudent(GetAuthorizationToken());
                return new MasterActivityStudentResponse()
                {
                    MasterActivtiesStudent = activites
                };
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new MasterActivityStudentResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }
        }

        [Route("studentinteractionasform")]
        [HttpPost]
        public async Task<BaseResponse> PostStudentInteractionAsFormData()
        {

            var studentInteractionRequest = new StudentInteractionRequest();
            //studentInteractionRequest.StudentIDs = HttpContext.Current.Request.Form["StudentID"];
            //studentInteractionRequest.ActivityIDs = HttpContext.Current.Request.Form["ActivityID"];
            //studentInteractionRequest.Notes = HttpContext.Current.Request.Form["Notes"];
            //studentInteractionRequest.LogDateTime = HttpContext.Current.Request.Form["LogDateTime"];


            //create interaction model
            //loop interaction model and call SP to save record, get return ID
            //save image to azure storage, get reference and save to doc activity table

            // Check if the request contains multipart/form-data.
            if (Request.Content.IsMimeMultipartContent("form-data"))
            {
                var accountName = "soteriafilestorage"; // ConfigurationManager.AppSettings["MobileUploadFileShareName"];
                var accountKey = "mPNCUQdUcqIHw7+7hYQ26edn7LWRTe6kNbweTUVCr2L56iLIxnYFxRkwR1YMW7T7lkKPKlgR3/0fTatLPXaJWQ=="; // ConfigurationManager.AppSettings["storage:account:key"];
                var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer imagesContainer = blobClient.GetContainerReference("mobileuploads");
                var provider = new AzureStorageMultipartFormDataStreamProvider(imagesContainer);


                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                }
                catch (Exception ex)
                {
                    //return BadRequest($"An error has occured. Details: {ex.Message}");
                }

                // Retrieve the filename of the file you have uploaded
                var filename = provider.FileData.FirstOrDefault()?.LocalFileName;


                /*string root = HttpContext.Current.Server.MapPath("~/App_Data");
                var provider = new MultipartFormDataStreamProvider(root);

                // Read the form data and return an async task.
                var task = Request.Content.ReadAsMultipartAsync(provider).
                    ContinueWith<HttpResponseMessage>(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                        }

                        // This illustrates how to get the file names.
                        foreach (MultipartFileData file in provider.FileData)
                        {
                            //Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                            //Trace.WriteLine("Server file path: " + file.LocalFileName);
                        }
                        return Request.CreateResponse(HttpStatusCode.OK);
                    });

                return task;*/
            }
            return new BaseResponse();
        }

        [Route("studentinteraction")]
        [HttpPost]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public BaseResponse StudentInteraction(StudentInteractionRequest studentInteractionRequest)
        {
            try
            {
                if (studentInteractionRequest.StudentID == null || studentInteractionRequest.StudentID.Count == 0)
                    throw new Exception("Invalid payload");

                StudentContext.PostStudentInteraction(GetAuthorizationToken(), studentInteractionRequest);
                return new BaseResponse();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new BaseResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }
        }

        [Route("studentsearch")]
        [HttpGet]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public StudentDetailsResponse SearchStudent(string searchText)
        {
            try
            {
                var searchName = new StudentSearchRequest()
                {
                    Name = searchText
                };
                var studentInfo = StudentContext.GetStudentDetails(GetAuthorizationToken(), searchName);

                if (studentInfo == null || studentInfo.Count == 0)
                {
                    var searchID = new StudentSearchRequest()
                    {
                        StudentSchoolID = searchText
                    };
                    studentInfo = StudentContext.GetStudentDetails(GetAuthorizationToken(), searchID);
                }

                return new StudentDetailsResponse()
                {
                    Students = studentInfo
                };
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new StudentDetailsResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }
        }

        [Route("studentidcardassign")]
        [HttpPost]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public BaseResponse AssignIdToStudent(StudentIDCardMapping studentIDCardMapping)
        {            
            try
            {
                var msg = StudentContext.AssignIdToStudent(GetAuthorizationToken(), studentIDCardMapping);
                if (string.IsNullOrEmpty(msg))
                    return new BaseResponse();
                else
                    throw new Exception(msg);                
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new BaseResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }
            
        }


        [Route("idcardavailabilitycheck")]
        [HttpPost]
        [HeaderAuthentication(RoleType.SuperAdmin, RoleType.SchoolAdmin, RoleType.SRO)]
        public IDCardAvailabilityCheckResponse IDCardAvailabilityCheck(IDCardAvailabilityCheckRequestPayload iDCardAvailabilityCheckRequestPayload)
        {
            try
            {
                var response = StudentContext.CheckIDCardAvailability(GetAuthorizationToken(), iDCardAvailabilityCheckRequestPayload);
                return new IDCardAvailabilityCheckResponse()
                {
                    IDCardAvailabilityCheck = response
                };
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, -1, 1, ExceptionSeverity.Exception);
                return new IDCardAvailabilityCheckResponse()
                {
                    OperationResult = GetFailureResult(ex.Message)
                };
            }            
        }
    }
}
