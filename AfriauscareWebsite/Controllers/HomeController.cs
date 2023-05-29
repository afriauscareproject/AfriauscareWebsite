using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.DataBaseLayer;
using Afriauscare.BusinessLayer.Gallery;

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
            return View();
        }
    }
}