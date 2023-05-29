using Afriauscare.BusinessLayer.Error;
using Afriauscare.BusinessLayer.User;
using Afriauscare.DataBaseLayer;
using System;
using System.Web.Mvc;
using System.Web.Security;
using Afriauscare.BusinessLayer.Shared;
using Afriauscare.DataBaseLayer.Shared;
using System.Collections.Generic;

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
                    var user = objUserDao.getUserbyUserandPassword(objUserModel);
                    if (user == null)
                    {
                        ModelState.AddModelError("Error", "The user email and password does not match or do not exists.");
                        return View();
                    }
                    else
                    {
                        Session["UserEmail"] = user.UserEmail;
                        Session["UserId"] = user.UserId;

                        return RedirectToAction("Index","HomeAdminPortal");
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