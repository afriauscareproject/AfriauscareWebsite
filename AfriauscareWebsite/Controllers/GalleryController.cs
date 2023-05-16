using System;
using System.Collections.Generic;
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
            
            try
            {
                GalleryDAO objGalleryDao = new GalleryDAO();
                GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();
                GalleryModel objGalleryEmptyModel = new GalleryModel()
                {
                    GalleryTitle = string.Empty,
                    GalleryDescription = string.Empty,
                    GalleryEventDate = DateTime.Today
                };

                if (ModelState.IsValid)
                {
                    var galleryId = objGalleryDao.CreateGallery(objGalleryModel);
                    int index = 1;
                    byte[] fileBytes;

                    foreach (var file in objGalleryModel.imageList)
                    {
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
                        index = index + 1;
                    }

                    ModelState.Clear();
                    TempData["GalleryAlertMessage"] = "Gallery Created Successfully...";

                    return View("CreateGallery", objGalleryEmptyModel);
                }
                return View();
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