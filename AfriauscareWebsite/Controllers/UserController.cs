using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.User;
using Afriauscare.BusinessLayer.Error;
using Afriauscare.DataBaseLayer;

namespace AfriauscareWebsite.Controllers
{
    public class UserController : Controller
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

        public ActionResult CreateUser()
        {
            UserModel objUserModel = new UserModel();
            objUserModel.UserActive = true;
            return View(objUserModel);
        }

        [HttpPost]
        public ActionResult CreateUser(UserModel objUserModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDAO objUserDAO = new UserDAO();
                    objUserDAO.CreateUser(objUserModel);

                    ModelState.Clear();
                    UserModel objEmptyUserModel = new UserModel()
                    {
                        Username = string.Empty,
                        UserLastName = string.Empty,
                        UserEmail = string.Empty,
                        UserPassword = string.Empty,
                        UserActive = false
                    };
                    TempData["UserAlertMessage"] = "User Created Successfully...";
                    return View("CreateUser", objEmptyUserModel);
                }
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            
            return View("CreateUser");

        }

        public ActionResult ViewUser()
        {
            UserDAO objUserDAO = new UserDAO();

            try
            {
                var userList = objUserDAO.getUsersAll();
                return View(userList);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

        [HttpGet]
        public ActionResult ModifyUser(int Id)
        {
            UserDAO objUserDAO = new UserDAO();
            try
            {
                var objUserModel = objUserDAO.getUserbyUserId(Id);
                return View(objUserModel);
            }
            catch(Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

        [HttpPost]
        public ActionResult ModifyUser(UserModel objUser)
        {
            UserDAO objUserDAO = new UserDAO();

            try
            {
                if (ModelState.IsValid)
                {
                    objUserDAO.ModifyUser(objUser);

                    ModelState.Clear();
                    UserModel objEmptyUserModel = new UserModel()
                    {
                        Username = string.Empty,
                        UserLastName = string.Empty,
                        UserEmail = string.Empty,
                        UserPassword = string.Empty,
                        UserActive = false
                    };
                    TempData["UserAlertMessage"] = "User Modifed Successfully...";

                    return View("ModifyUser", objEmptyUserModel);
                }
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            return View("ModifyUser");
        }

        [HttpGet]
        public ActionResult DisableUser(int Id)
        {
            UserDAO objUserDAO = new UserDAO();

            var objUserModel = objUserDAO.getUserbyUserId(Id);
            return View(objUserModel);
        }

        [HttpPost]
        public JsonResult DisableUserJson(int UserId)
        {
            bool result = false;
            UserDAO objUserDAO = new UserDAO();
            try
            {
                objUserDAO.DisableUser(UserId);
                result = true;
                TempData["UserAlertMessage"] = "User Deactivated Successfully...";

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}