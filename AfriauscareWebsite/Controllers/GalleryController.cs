using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.Gallery;
using Afriauscare.BusinessLayer.Error;
using Afriauscare.DataBaseLayer;
using System.IO;

namespace AfriauscareWebsite.Controllers
{
    public class GalleryController : Controller
    {
        //Function that validates if the session is created/active. If not active/valid, it will redirect to the EndSession View
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserEmail"] != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                RedirectToAction("EndSession", "HomeAdminPortal");
            }
        }

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

        [HttpPost]
        public ActionResult CreateGallery(GalleryModel objGalleryModel)
        {
            GalleryDAO objGalleryDao = new GalleryDAO();
            GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();

            if (ModelState.IsValid)
            {
                var galleryId = objGalleryDao.CreateGallery(objGalleryModel);

                foreach (var file in objGalleryModel.imageList)
                {
                    byte[] fileBytes;
                    int index = 0;

                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        fileBytes = br.ReadBytes(file.ContentLength);
                    }

                    GalleryContentModel objGallerycontent = new GalleryContentModel
                    {
                        GalleryContentImage = fileBytes,
                        GalleryContentIndex = index,
                        GalleryContentIsActive = true,
                        GalleryContentPath = string.Empty
                    };
                    objGalleryContentDao.CreateGalleryContent(objGallerycontent, galleryId);
                    index = index++;
                }
            }

            return View();
        }
    }
}