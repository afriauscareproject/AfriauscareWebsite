using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Afriauscare.DataBaseLayer;
using Afriauscare.BusinessLayer.Gallery;
using Afriauscare.BusinessLayer.BankInformation;
using Afriauscare.DataBaseLayer.BankInformation;
using Afriauscare.DataBaseLayer.ContactInformation;
using Afriauscare.BusinessLayer.ContactInformation;
using Afriauscare.DataBaseLayer.Shared;
using Afriauscare.BusinessLayer.Error;

namespace AfriauscareWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;
            }
            catch(Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            

            return View();
        }

        public ActionResult About()
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            return View();
        }

        public ActionResult Contact()
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();
                objContactModel.State_name = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                objContactModel.Suburb_name = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;

                return View(objContactModel);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

        public ActionResult Gallery()
        {
            GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();
            List<GalleryContentModel> list = objGalleryContentDao.getImagesAll();

            return View(list);
        }

        public ActionResult Gallery2()
        {
            GalleryDAO objGalleryDao = new GalleryDAO();
            GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();
            GalleryContentModel objGalleryContent = new GalleryContentModel();

            try
            {
                List<GalleryModel> list = objGalleryDao.getGalleryAll();

                foreach (var item in list)
                {
                    objGalleryContent = objGalleryContentDao.getFirstImageFromGallery(item.GalleryId);
                    item.DefaultImage = objGalleryContent.GalleryContentImage;
                }

                ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
                StatesDAO objStateDao = new StatesDAO();
                SuburbsDAO objSuburbDao = new SuburbsDAO();
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;

                return View(list);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            
        }

        public ActionResult Donate()
        {
            BankInformationDAO objBankDAO = new BankInformationDAO();
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                BankInformationModel objBankModel = objBankDAO.GetBankInformationDefault();
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();
                objBankModel.Phone_Number = objContactModel.Phone_number;
                objBankModel.Mobile_Number = objContactModel.Mobile_number;

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;

                return View(objBankModel);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            
        }

        public ActionResult BoardMembers()
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;
                TempData["MobilePhone"] = objContactModel.Mobile_number;
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            return View();
        }

        public ActionResult Team()
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;
                TempData["MobilePhone"] = objContactModel.Mobile_number;
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            return View();
        }

        public ActionResult KeyPartners()
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            return View();
        }

        public ActionResult ProgramServices()
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            return View();
        }

        public ActionResult ViewPictures(int galleryId)
        {
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();

                TempData["Address"] = objContactModel.Contact_address;
                TempData["Suburb"] = objSuburbDao.GetSuburbNameById(Int16.Parse(objContactModel.Suburb_id));
                TempData["State"] = objStateDao.GetStateNameById(Int16.Parse(objContactModel.State_id));
                TempData["Postcode"] = objContactModel.Postcode;
                TempData["Phone"] = objContactModel.Phone_number;
                TempData["Email"] = objContactModel.Email_address;

                GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();
                List<GalleryContentModel> list = objGalleryContentDao.getImagesFromGallery(galleryId);

                return View("ViewPictures", list);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

    }
}