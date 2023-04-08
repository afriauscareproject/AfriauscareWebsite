using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AfriauscareWebsite.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [DisplayName("First Name :")]
        public string Username { get; set; }

        [DisplayName("Last Name :")]
        public string UserLastName { get; set;}

        [DisplayName("Email :")]
        [Required(ErrorMessage ="Email is Required.")]
        public string UserEmail { get; set; }

        [DisplayName("Password :")]
        [Required(ErrorMessage ="Password is Required.")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [DisplayName("User Active?")]
        public Boolean UserActive { get; set; }

        public string message { get; set; }
    }
}