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
                        Postcode = item.postcode,
                        Is_default = item.is_default
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
                    postcode = objContactModel.Postcode,
                    is_default = objContactModel.Is_default
                };

                DataBase.contact_information.Add(objContactInformation);
                DataBase.SaveChanges();
            }

        }

        public ContactInformationModel GetContactInformationbyId(int ContactId)
        {
            var contactInformation = new contact_information();
            ContactInformationModel objContactModel = new ContactInformationModel();

            using (var DataBase = new AfriAusEntities())
            {
                contactInformation = DataBase.contact_information.Where(u => u.contact_id == ContactId).FirstOrDefault();
                objContactModel.Contact_id = contactInformation.contact_id;
                objContactModel.Email_address = contactInformation.email_address;
                objContactModel.Phone_number = contactInformation.phone_number;
                objContactModel.Mobile_number = contactInformation.mobile_number;
                objContactModel.Fax_number = contactInformation.fax_number;
                objContactModel.Contact_address = contactInformation.contact_address;
                objContactModel.State_id = contactInformation.state_id.ToString();
                objContactModel.Suburb_id = contactInformation.suburb_id.ToString();
                objContactModel.Postcode = contactInformation.postcode;
                objContactModel.Is_default = contactInformation.is_default;
            }

            return objContactModel;
        }

        public void ModifyContactInformation(ContactInformationModel objModel)
        {
            using (var DataBase = new AfriAusEntities())
            {
                contact_information objContactInformation = new contact_information()
                {
                    contact_id = objModel.Contact_id,
                    email_address = objModel.Email_address,
                    phone_number = objModel.Phone_number,
                    mobile_number = objModel.Mobile_number,
                    fax_number = objModel.Fax_number,
                    contact_address = objModel.Contact_address,
                    state_id = Int16.Parse(objModel.State_id),
                    suburb_id = Int16.Parse(objModel.Suburb_id),
                    postcode = objModel.Postcode,
                    is_default = objModel.Is_default
                };

                DataBase.contact_information.Add(objContactInformation);
                DataBase.Entry(objContactInformation).State = System.Data.EntityState.Modified;
                DataBase.SaveChanges();
            }
        }

        public void ClearIsDefaultField()
        {
            using (var Database = new AfriAusEntities())
            {
                var listDefaultValues = Database.contact_information.Where(c => c.is_default == true);
                foreach (var item in listDefaultValues)
                {
                    item.is_default = false;
                }
                Database.SaveChanges();
            }
        }

        public ContactInformationModel GetContactInformationDefault()
        {
            using (var DataBase = new AfriAusEntities())
            {
                var data = DataBase.contact_information.Where(c => c.is_default == true).FirstOrDefault();

                ContactInformationModel objContactInformation = new ContactInformationModel()
                {
                    Contact_id = data.contact_id,
                    Email_address = data.email_address,
                    Phone_number = data.phone_number,
                    Mobile_number = data.mobile_number,
                    Fax_number = data.fax_number,
                    Contact_address = data.contact_address,
                    State_id = data.state_id.ToString(),
                    Suburb_id = data.suburb_id.ToString(),
                    Postcode = data.postcode,
                    Is_default = data.is_default
                };

                return objContactInformation;
            }
        }

        public void DeleteContactInformation(int contactInformationId)
        {
            using (var DataBase = new AfriAusEntities())
            {
                if (DataBase.contact_information.Any(c => c.contact_id == contactInformationId))
                {
                    contact_information recordToDelete = new contact_information() { contact_id = contactInformationId };
                    DataBase.Entry(recordToDelete).State = System.Data.EntityState.Deleted;
                    DataBase.SaveChanges();
                }
            }
        }
    }
}
