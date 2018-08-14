using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTutorial.Models
{
    public class UserProfileModelClass
    {
        [Required]
        public int UserID { get; set; }

        public string UserName { get; set; }

        [Required]
        [Remote("EditCheckEmailAddress", "User", AdditionalFields = "UserID", ErrorMessage = "Email Address Already Added.")]
        public string EmailAddress { get; set; }

        [Required]
        [Remote("EditCheckMobileNo", "User", AdditionalFields = "UserID", ErrorMessage = "Mobile No already registered.")]
        public string MobileNo { get; set; }

        [Required]
        public string Address { get; set; }

    }
}