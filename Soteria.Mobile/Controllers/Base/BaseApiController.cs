using Soteria.DataComponents.Infrastructure.Resource;
using Soteria.DataComponents.ViewModel;
using Soteria.DataComponents.ViewModel.Base;
using Soteria.Mobile.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace Soteria.Mobile.Controllers
{
    /// <summary>
    /// Base API Controller
    /// </summary>
    public class BaseApiController : ApiController
    {
        #region Properties
        /// <summary>
        /// User Session Variable
        /// </summary>
        public UserSession CurrentSession
        {
            get { return System.Web.HttpContext.Current.User as UserSession; }
        }

        
        #endregion

        #region Public API Methods
        /// <summary>
        /// Get Success Result
        /// </summary>
        /// <returns></returns>
        public OperationResult GetSuccessResult(string RespMessage = "")
        {
            return new OperationResult()
            {
                IsSuccess = true,
                ResponseMessage = string.IsNullOrEmpty(RespMessage) ? ErrorMessages.OperationSuccess : RespMessage
            };
        }
        /// <summary>
        /// Get Success Response
        /// </summary>
        /// <returns></returns>
        public BaseResponse GetSuccessResponse(string RespMessage = "")
        {
            return new BaseResponse()
            {
                OperationResult = GetSuccessResult(RespMessage)
            };
        }
        /// <summary>
        /// Get Failure Result
        /// </summary>
        /// <returns></returns>
        public OperationResult GetFailureResult(string ErrMessage = "")
        {
            return new OperationResult()
            {
                IsSuccess = false,
                ResponseMessage = string.IsNullOrEmpty(ErrMessage) ? ErrorMessages.OperationFailed : ErrMessage
            };
        }
        /// <summary>
        /// Get Invalid Result
        /// </summary>
        /// <returns></returns>
        public OperationResult GetInvalidResult(string ErrMessage = "")
        {
            return new OperationResult()
            {
                IsSuccess = false,
                ResponseMessage = string.IsNullOrEmpty(ErrMessage) ? ErrorMessages.InvalidRequest : ErrMessage
            };
        }
        /// <summary>
        /// Get Failure Response
        /// </summary>
        /// <returns></returns>
        public BaseResponse GetFailureResponse(string ErrMessage = "")
        {
            return new BaseResponse()
            {
                OperationResult = GetFailureResult(ErrMessage)
            };
        }

        public AuthorizationToken GetAuthorizationToken()
        {
            return new AuthorizationToken()
            {
                SchoolID = CurrentSession.SchoolID,
                SchoolDistrictID = CurrentSession.SchoolDistrictID,
                UserID = CurrentSession.UserID,
                RoleID = CurrentSession.RoleID
            };
        }

        #endregion
    }

}
