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
            ViewBag.Message = "Your application description page.";

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
    }
}