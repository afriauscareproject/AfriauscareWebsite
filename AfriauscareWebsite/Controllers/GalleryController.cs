using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.Gallery;
using Afriauscare.BusinessLayer.Error;
using Afriauscare.DataBaseLayer;

namespace AfriauscareWebsite.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult ViewGallery()
        {
            List<GalleryModel> listGallery = new List<GalleryModel>();
            GalleryDAO objGalleryDAO = new GalleryDAO();

            listGallery = objGalleryDAO.getGalleryAll();

            return View(listGallery);
        }

        public ActionResult CreateGallery()
        {
            return View();
        }
    }
}