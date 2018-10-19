using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.ViewModel.Common
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Organization code is required.")]
        public string LoginCode { get; set; }
        public string IPAddress { get; set; }
    }

    public class ApplicationUser
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WelcomeName { get; set; }
        public string PhoneNumber { get; set; }
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public long SchoolID { get; set; }
        public string SchoolName { get; set; }
        public long SchoolDistrictID { get; set; }
        public string SchoolDistrictName { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolDistrictCode { get; set; }
    }

    public class AppConfig
    {
        public string SchoolTimeZone { get; set; }
        public decimal BeaconSignalThreshold { get; set; }
        public decimal BadgeSignalThreshold { get; set; }
        public string UUID { get; set; }
        public string IDCardUUID { get; set; }
        public bool HasAccessSRO { get; set; }
        public bool HasAccessIssueID { get; set; }
        public bool HasAccessStudentInteraction { get; set; }
    }


    public class AuthenticationResponse : BaseResponse
    {
        public string TokenID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

    public class AppConfigResponse : BaseResponse
    {
        public AppConfig AppConfig { get; set; }
    }
}
