using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Afriauscare.BusinessLayer.User
{
    /// <summary>
    /// User class model for User module
    /// </summary>
    public class UserModel
    {
        public int UserId { get; set; }

        [DisplayName("First Name")]
        [StringLength(30, ErrorMessage = "The maximum lenght for First Name is 30 characters.")]
        public string Username { get; set; }

        [DisplayName("Last Name")]
        [StringLength(30, ErrorMessage = "The maximum lenght for Last Name is 30 characters.")]
        public string UserLastName { get; set;}

        [DisplayName("Email")]
        [Required(ErrorMessage ="Email is Required.")]
        [StringLength(50,ErrorMessage = "The maximum lenght for Email is 50 characters")]
        public string UserEmail { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage ="Password is Required.")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The maximum lenght for Password is 30 characters.")]
        public string UserPassword { get; set; }

        [DisplayName("User Active?")]
        public bool UserActive { get; set; }

        public string message { get; set; }
    }
}