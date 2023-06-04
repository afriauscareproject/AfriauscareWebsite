using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Afriauscare.BusinessLayer.ContactInformation
{
    /// <summary>
    /// Class model for Contact Information Creation
    /// </summary>
    public class ContactInformationModel
    {
        public int Contact_id { get; set; }

        [DisplayName("Phone Number")]
        [StringLength(30, ErrorMessage = "The maximum lenght for Phone Number is 30 characters.")]
        public string Phone_number { get; set; }

        [DisplayName("Mobile Number")]
        [StringLength(30, ErrorMessage = "The maximum lenght for Mobile Number is 30 characters.")]
        public string Mobile_number { get; set; }

        [DisplayName("Fax Number")]
        [StringLength(30, ErrorMessage = "The maximum lenght for Fax Number is 30 characters.")]
        public string Fax_number { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "The maximum lenght for Email Address is 50 characters.")]
        public string Email_address { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "The maximum lenght for Address is 100 characters.")]
        public string Contact_address { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "State is required")]
        public string State_id { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        [DisplayName("Suburb")]
        [Required(ErrorMessage = "Suburb is required")]
        public string Suburb_id { get; set; }
        public IEnumerable<SelectListItem> Suburbs { get; set; }

        [Required(ErrorMessage ="Postcode is required")]
        [DisplayName("Postcode")]
        [StringLength(10, ErrorMessage = "The maximum lenght for Postcode is 10 characters.")]
        public string Postcode { get; set; }

        [DisplayName("Is default?")]
        public bool Is_default { get; set; }

        public string State_name { get; set; }

        public string Suburb_name { get; set; }
    }
}
