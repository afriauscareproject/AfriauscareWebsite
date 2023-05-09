using Afriauscare.BusinessLayer.Error;
using Afriauscare.BusinessLayer.User;
using Afriauscare.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AfriauscareWebsite.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }

        // GET: User
        public ActionResult Login()
        {
            UserModel objUserModel = new UserModel();
            return View(objUserModel);
        }

        [HttpPost]
        public ActionResult Login(UserModel objUserModel)
        {
            UserDAO objUserDao = new UserDAO();

            try
            {
                if (ModelState.IsValid)
                {
                    if (objUserDao.getUserbyUserandPassword(objUserModel) == null)
                    {
                        ModelState.AddModelError("Error", "The user email and password does not match or do not exists.");
                        return View();
                    }
                    else
                    {
                        Session["UserEmail"] = objUserModel.UserEmail;
                        return RedirectToAction("Index", "HomeAdminPortal");
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "HomeAdminPortal");
        }

        public ActionResult EndSession()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Session");
        }
    }
}