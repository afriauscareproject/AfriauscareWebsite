using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.User;
using Afriauscare.DataBaseLayer;

namespace AfriauscareWebsite.Controllers
{
    public class UserController : Controller
    {
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

            if (ModelState.IsValid)
            {
                if (objUserDao.getUserbyUserandPassword(objUserModel) == null)
                {
                    ModelState.AddModelError("Error", "The user email and password does not match or do not exists.");
                    return View();
                }
                else
                {
                    Session["Email"] = objUserModel.UserEmail;
                    return RedirectToAction("Index", "HomeAdminPortal");
                }

            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "HomeAdminPortal");
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
            if (ModelState.IsValid)
            {
                UserDAO objUserDAO = new UserDAO();
                objUserDAO.CreateUser(objUserModel);

                objUserModel.Username = string.Empty;
                objUserModel.UserLastName = string.Empty;
                objUserModel.UserEmail = string.Empty;
                objUserModel.UserPassword = string.Empty;
                objUserModel.UserActive = false;
                objUserModel.message = "User added successfully!";
                return View(objUserModel);
            }
            return View("CreateUser");

        }

        public ActionResult ViewUser()
        {
            UserDAO objUserDAO = new UserDAO();
            var list = objUserDAO.getUsersAll();

            return View(list);
        }

        [HttpGet]
        public ActionResult ModifyUser(int Id)
        {
            UserDAO objUserDAO = new UserDAO();

            var objUserModel = objUserDAO.getUserbyUserId(Id);
            return View(objUserModel);
        }

        [HttpPost]
        public ActionResult ModifyUser(User objUser)
        {
            UserDAO objUserDAO = new UserDAO();

            if (ModelState.IsValid)
            {
                objUserDAO.ModifyUser(objUser);
            }

            var list = objUserDAO.getUsersAll();
            return View("ViewUser", list);
        }

        [HttpGet]
        public ActionResult DisableUser(int Id)
        {
            UserDAO objUserDAO = new UserDAO();

            var objUserModel = objUserDAO.getUserbyUserId(Id);
            return View(objUserModel);
        }

        [HttpPost]
        public ActionResult DisableUser(User objUser)
        {
            UserDAO objUserDAO = new UserDAO();

            if (ModelState.IsValid)
            {
                objUserDAO.DisableUser(objUser.UserId);
            }

            var list = objUserDAO.getUsersAll();
            return View("ViewUser", list);
        }
    }
}