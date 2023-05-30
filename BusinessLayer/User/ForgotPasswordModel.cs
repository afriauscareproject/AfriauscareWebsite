using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Afriauscare.BusinessLayer.User
{
    public class ForgotPasswordModel
    {
        public int UserId { get; set; }

        [DisplayName("First Name")]
        [StringLength(30, ErrorMessage = "The maximum lenght for First Name is 30 characters.")]
        public string Username { get; set; }

        [DisplayName("Last Name")]
        [StringLength(30, ErrorMessage = "The maximum lenght for Last Name is 30 characters.")]
        public string UserLastName { get; set;}

        [DisplayName("Email")]
        [StringLength(50,ErrorMessage = "The maximum lenght for Email is 50 characters")]
        public string UserEmail { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The maximum lenght for Password is 30 characters.")]
        public string UserPassword { get; set; }

        public string message { get; set; }

        [DisplayName("Temporary Password")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The maximum lenght for Password is 30 characters.")]
        public string TemporaryPassword { get; set; }
    }
}