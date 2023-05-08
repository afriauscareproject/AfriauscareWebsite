using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public string Email_address { get; set; }

        [DisplayName("Address")]
        public string Contact_address { get; set; }

        [DisplayName("State")]
        public int State_id { get; set; }

        [DisplayName("Suburb")]
        public int Suburb_id { get; set; }

        [DisplayName("Postcode")]
        public string Postcode { get; set; }
    }
}
