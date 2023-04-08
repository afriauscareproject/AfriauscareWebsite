using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AfriauscareWebsite.App_Data;
using AfriauscareWebsite.Models;

namespace AfriauscareWebsite.Controllers
{
    public class UserController : Controller
    {
        AfriAusEntities objDatabaseEntities = new AfriAusEntities();
        // GET: User
        public ActionResult Login()
        {
            UserModel objUserModel = new UserModel();
            return View(objUserModel);
        }

        [HttpPost]
        public ActionResult Login(UserModel objUserModel)
        {
            if(ModelState.IsValid)
            {
                if(objDatabaseEntities.Users.Where(m => m.UserEmail == objUserModel.UserEmail && m.UserPassword == objUserModel.UserPassword).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error","The user email and password does not match or do not exists.");
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
            if(ModelState.IsValid)
            {
                User objUserEntity = new User
                {
                    Username = objUserModel.Username,
                    UserLastName = objUserModel.UserLastName,
                    UserEmail = objUserModel.UserEmail,
                    UserPassword = objUserModel.UserPassword,
                    UserActive = objUserModel.UserActive
                };

                objDatabaseEntities.Users.Add(objUserEntity);
                objDatabaseEntities.SaveChanges();

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
    }
}