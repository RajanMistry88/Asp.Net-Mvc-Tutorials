using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTutorial.Models
{
    public class UserRegistrationModelClass
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [Remote("CheckEmailAddress", "User", ErrorMessage = "Email Address Already Added.")]
        public string EmailAddress { get; set; }

        [Required]
        [Remote("CheckMobileNo", "User", ErrorMessage = "Mobile No Already Added.")]
        public string MobileNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm New Password dose not match")]
        public string ConfirmPassword { get; set; }
    }
}