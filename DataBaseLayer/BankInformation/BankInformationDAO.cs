using System;
using System.Collections.Generic;
using System.Linq;
using Afriauscare.BusinessLayer.BankInformation;

namespace Afriauscare.DataBaseLayer.BankInformation
{
    public class BankInformationDAO
    {
        public List<BankInformationModel> GetBankInformation()
        {
            List<BankInformationModel> listReturn = new List<BankInformationModel>();

            using (var DataBase = new AfriAusEntities())
            {
                var BankInformationList = DataBase.bank_information.ToList();

                foreach (var item in BankInformationList)
                {
                    BankInformationModel objGallery = new BankInformationModel
                    {
                        Bank_information_id = item.bank_information_id,
                        Bank_id = item.bank_id,
                        Abn_number = item.abn_number,
                        Bsb_number = item.bsb_number,
                        Account_number = item.account_number,
                        Is_default = item.is_default
                    };
                    listReturn.Add(objGallery);
                }
            }

            return listReturn;
        }

        public void CreateBankInformation(BankInformationModel objModel)
        {
            using (var DataBase = new AfriAusEntities())
            {
                bank_information objBankInformation = new bank_information()
                {
                    bank_id = objModel.Bank_id,
                    abn_number = objModel.Abn_number,
                    bsb_number = objModel.Bsb_number,
                    account_number = objModel.Account_number,
                    is_default = objModel.Is_default
                };

                DataBase.bank_information.Add(objBankInformation);
                DataBase.SaveChanges();
            }
        }

        public void ClearIsDefaultField()
        {
            using (var Database = new AfriAusEntities())
            {
                var listDefaultValues = Database.bank_information.Where(b => b.is_default == true);
                foreach (var item in listDefaultValues)
                {
                    item.is_default = false;
                }
                Database.SaveChanges();
            }
        }

        public BankInformationModel GetBankInformationbyId(int bankInformationId)
        {
            var bankInformation = new bank_information();
            BankInformationModel objBankModel = new BankInformationModel();

            using (var DataBase = new AfriAusEntities())
            {
                bankInformation = DataBase.bank_information.Where(b => b.bank_information_id == bankInformationId).FirstOrDefault();
                objBankModel.Bank_information_id = bankInformation.bank_information_id;
                objBankModel.Bank_id = bankInformation.bank_id;
                objBankModel.Abn_number = bankInformation.abn_number;
                objBankModel.Bsb_number = bankInformation.bsb_number;
                objBankModel.Account_number = bankInformation.account_number;
                objBankModel.Is_default = bankInformation.is_default;
            }

            return objBankModel;
        }

        public void ModifyBankInformation(BankInformationModel objModel)
        {
            using (var DataBase = new AfriAusEntities())
            {
                bank_information objBankInformation = new bank_information()
                {
                    bank_information_id = objModel.Bank_information_id,
                    abn_number = objModel.Abn_number,
                    bsb_number = objModel.Bsb_number,
                    account_number = objModel.Account_number,
                    bank_id= objModel.Bank_id,
                    is_default = objModel.Is_default
                };

                DataBase.bank_information.Add(objBankInformation);
                DataBase.Entry(objBankInformation).State = System.Data.EntityState.Modified;
                DataBase.SaveChanges();
            }
        }
    }
}
