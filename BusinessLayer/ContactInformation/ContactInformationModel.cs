using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Afriauscare.BusinessLayer.ContactInformation
{
    public class ContactInformationModel
    {
        public int Contact_id { get; set; }

        [DisplayName("Phone Number")]
        public string Phone_number { get; set; }

        [DisplayName("Mobile Number")]
        public string Mobile_number { get; set; }

        [DisplayName("Fax Number")]
        public string Fax_number { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email_address { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
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
        public string Postcode { get; set; }

        [DisplayName("Is default?")]
        public bool Is_default { get; set; }
    }
}
