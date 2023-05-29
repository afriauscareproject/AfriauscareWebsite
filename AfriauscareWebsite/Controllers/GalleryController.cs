using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.Gallery;
using Afriauscare.BusinessLayer.Error;
using Afriauscare.DataBaseLayer;
using System.IO;
using Afriauscare.BusinessLayer.Shared;
using Afriauscare.DataBaseLayer.Shared;

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
                filterContext.Result = new RedirectResult("~/Session/EndSession");
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
                if (objGalleryModel.imageList[0] != null)
                {
                    int maxFileSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileSize"]);
                    ImageListValidation objImagelistVal = new ImageListValidation();
                    if (!objImagelistVal.FileSizeValidation(objGalleryModel.imageList, maxFileSize))
                    {
                        ModelState.AddModelError("imageList", "One or more files size are larger than 2MB.");
                    }

                    if (!objImagelistVal.FileExtensionValidation(objGalleryModel.imageList))
                    {
                        ModelState.AddModelError("imageList", "Image files are permitted only (png, jpg, jpeg).");
                    }
                }

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
                            GalleryContentPath = string.Empty,
                            GalleryFileName = file.FileName
                        };
                        objGalleryContentDao.CreateGalleryContent(objGallerycontent, galleryId);
                        index = index + 1;
                    }

                    LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                    LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                    {
                        User_id = Int16.Parse(Session["UserId"].ToString()),
                        Module_Name = "Gallery",
                        Action_Excuted = "Create",
                        Datetime_action = DateTime.Now
                    };

                    objLogUserDao.CreateLogUserActivity(objLogUserModel);

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

        public ActionResult ViewGalleryContent(int galleryId)
        {
            GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();
            List<GalleryContentModel> list = objGalleryContentDao.getImagesFromGallery(galleryId);

            return PartialView("ViewGalleryContent", list);
        }

        [HttpGet]
        public ActionResult ModifyGallery(int GalleryId)
        {
            GalleryDAO objGalleryDAO = new GalleryDAO();
            try
            {
                var objGalleryModel = objGalleryDAO.GetGalleryById(GalleryId);

                return View(objGalleryModel);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel
                {
                    ErrorMessage = ex.Message
                };
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

        [HttpPost]
        public ActionResult ModifyGallery(GalleryModifyModel objGalleryModel)
        {
            try
            {
                if (objGalleryModel.imageList[0] != null)
                {
                    int maxFileSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileSize"]);
                    ImageListValidation objImagelistVal = new ImageListValidation();
                    if (!objImagelistVal.FileSizeValidation(objGalleryModel.imageList, maxFileSize))
                    {
                        ModelState.AddModelError("imageList", "One or more files size are larger than 2MB.");
                    }

                    if (!objImagelistVal.FileExtensionValidation(objGalleryModel.imageList))
                    {
                        ModelState.AddModelError("imageList", "Image files are permitted only (png, jpg, jpeg).");
                    }
                }

                if (ModelState.IsValid)
                {
                    GalleryDAO objGalleryDao = new GalleryDAO();
                    GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();
                    GalleryModifyModel objGalleryEmptyModel = new GalleryModifyModel()
                    {
                        GalleryTitle = string.Empty,
                        GalleryDescription = string.Empty,
                        GalleryEventDate = DateTime.Today
                    };

                    objGalleryDao.ModifyGallery(objGalleryModel);

                    if (objGalleryModel.imageList[0] != null)
                    {
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
                                GalleryContentIndex = 0,
                                GalleryContentIsActive = true,
                                GalleryContentPath = string.Empty,
                                GalleryFileName = file.FileName
                            };
                            objGalleryContentDao.CreateGalleryContent(objGallerycontent, objGalleryModel.GalleryId);
                        }

                        objGalleryContentDao.AssignImagesIndexes(objGalleryModel.GalleryId);
                    }

                    LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                    LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                    {
                        User_id = Int16.Parse(Session["UserId"].ToString()),
                        Module_Name = "Gallery",
                        Action_Excuted = "Modify",
                        Datetime_action = DateTime.Now
                    };

                    objLogUserDao.CreateLogUserActivity(objLogUserModel);

                    ModelState.Clear();
                    TempData["GalleryAlertMessage"] = "Gallery Updated Successfully...";

                    return View("ModifyGallery", objGalleryEmptyModel);
                }
                else
                {
                    return View("ModifyGallery");
                }
            }
            catch(Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

        public ActionResult ViewGalleryContentDelete(int galleryId)
        {
            GalleryContentDAO objGalleryContentDao = new GalleryContentDAO();
            List<GalleryContentModel> list = objGalleryContentDao.getImagesFromGalleryAll(galleryId);

            return PartialView("DeleteImages", list);
        }

        [HttpPost]
        public ActionResult DeleteImages(IList<GalleryContentModel> model)
        {
            int galleryId = 0;
            GalleryContentDAO objGalleryDao = new GalleryContentDAO();

            foreach (var item in model)
            {
                galleryId = item.GalleryId;

                if (item.GalleryContentIsActive)
                {
                    objGalleryDao.DeleteGalleryContent(item.GalleryContentId);
                }
               
            }

            objGalleryDao.AssignImagesIndexes(galleryId);
            GalleryDAO objGalleryDAO = new GalleryDAO();
            var objGalleryModel = objGalleryDAO.GetGalleryById(galleryId);

            LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
            LogUserActivityModel objLogUserModel = new LogUserActivityModel()
            {
                User_id = Int16.Parse(Session["UserId"].ToString()),
                Module_Name = "Gallery",
                Action_Excuted = "DeleteImages",
                Datetime_action = DateTime.Now
            };

            objLogUserDao.CreateLogUserActivity(objLogUserModel);

            TempData["GalleryAlertMessage"] = "Images Deleted Successfully...";

            return View("ModifyGallery", objGalleryModel);
        }

        [HttpPost]
        public JsonResult DeleteGalleryJson(int GalleryId)
        {
            bool result = false;
            GalleryDAO objGalleryDAO = new GalleryDAO();
            GalleryContentDAO objGalleryContentDAO = new GalleryContentDAO();

            try
            {
                objGalleryContentDAO.DeleteGalleryContentByGalleryId(GalleryId);
                objGalleryDAO.DeleteGallery(GalleryId);
                result = true;

                LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                {
                    User_id = Int16.Parse(Session["UserId"].ToString()),
                    Module_Name = "Gallery",
                    Action_Excuted = "Delete",
                    Datetime_action = DateTime.Now
                };

                objLogUserDao.CreateLogUserActivity(objLogUserModel);

                TempData["GalleryAlertMessage"] = "Gallery Deleted Successfully...";

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
    }
}