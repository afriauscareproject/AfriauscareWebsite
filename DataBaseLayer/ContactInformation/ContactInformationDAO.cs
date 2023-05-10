using System;
using System.Collections.Generic;
using System.Linq;
using Afriauscare.BusinessLayer.ContactInformation;

namespace Afriauscare.DataBaseLayer
{
    public class ContactInformationDAO
    {
        public List<ContactInformationModel> GetContactInformationAll()
        {
            List<ContactInformationModel> listContact = new List<ContactInformationModel>();

            using (var DataBase = new AfriAusEntities())
            {
                var list = DataBase.contact_information.ToList();

                foreach (var item in list)
                {
                    ContactInformationModel objUser = new ContactInformationModel()
                    {
                        Contact_id = item.contact_id,
                        Email_address = item.email_address,
                        Phone_number = item.phone_number,
                        Mobile_number = item.mobile_number,
                        Fax_number = item.fax_number,
                        Contact_address = item.contact_address,
                        State_id = item.state_id.ToString(),
                        Suburb_id = item.suburb_id.ToString(),
                        Postcode = item.postcode
                    };
                    listContact.Add(objUser);
                }
            }

            return listContact;
        }

        public void CreateContactInformation(ContactInformationModel objContactModel)
        {
            using (var DataBase = new AfriAusEntities())
            {
                contact_information objContactInformation = new contact_information
                {
                    email_address = objContactModel.Email_address,
                    phone_number = objContactModel.Phone_number,
                    mobile_number = objContactModel.Mobile_number,
                    fax_number = objContactModel.Fax_number,
                    contact_address = objContactModel.Contact_address,
                    state_id = Int16.Parse(objContactModel.State_id),
                    suburb_id = Int16.Parse(objContactModel.Suburb_id),
                    postcode = objContactModel.Postcode
                };

                DataBase.contact_information.Add(objContactInformation);
                DataBase.SaveChanges();
            }

        }
    }
}
