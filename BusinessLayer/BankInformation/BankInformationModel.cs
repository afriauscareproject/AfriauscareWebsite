using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Afriauscare.BusinessLayer.BankInformation
{
    public class BankInformationModel
    {
        public int Bank_information_id { get; set; }

        [DisplayName("Bank")]
        [Required(ErrorMessage = "Bank is required")]
        public int Bank_id { get; set; }

        public IEnumerable<SelectListItem> Banks { get; set; }

        [DisplayName("ABN Number")]
        [Required(ErrorMessage = "ABN Number is required")]
        [StringLength(30, ErrorMessage = "The maximum lenght for ABN Number is 30 characters.")]
        public string Abn_number { get; set; }

        [DisplayName("BSB Number")]
        [Required(ErrorMessage = "BSB Number is required")]
        [StringLength(30, ErrorMessage = "The maximum lenght for BSB Number is 30 characters.")]
        public string Bsb_number { get; set; }

        [DisplayName("Account Number")]
        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(30, ErrorMessage = "The maximum lenght for Account Number is 30 characters.")]
        public string Account_number { get; set; }

        [DisplayName("Is default?")]
        public bool Is_default { get; set; }

        public string Bank_Name { get; set; }

        public string Phone_Number { get; set; }

        public string Mobile_Number { get; set; }
    }
}
