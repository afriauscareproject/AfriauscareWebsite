using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.DataBaseLayer;
using Afriauscare.BusinessLayer.Gallery;
using Afriauscare.BusinessLayer.BankInformation;
using Afriauscare.DataBaseLayer.BankInformation;
using Afriauscare.DataBaseLayer.ContactInformation;
using Afriauscare.BusinessLayer.ContactInformation;

namespace AfriauscareWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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

            List<GalleryModel> list = objGalleryDao.getGalleryAll();

            foreach(var item in list)
            {
                objGalleryContent = objGalleryContentDao.getFirstImageFromGallery(item.GalleryId);
                item.DefaultImage = objGalleryContent.GalleryContentImage;
            }

            return View(list);
        }

        public ActionResult Donate()
        {
            BankInformationDAO objBankDAO = new BankInformationDAO();
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();

            BankInformationModel objBankModel = objBankDAO.GetBankInformationDefault();
            ContactInformationModel objContactModel = objContactInformationDAO.GetContactInformationDefault();
            objBankModel.Phone_Number = objContactModel.Phone_number;
            objBankModel.Mobile_Number = objContactModel.Mobile_number;

            return View(objBankModel);
        }
    }
}