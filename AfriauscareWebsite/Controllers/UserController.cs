﻿using System;
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
                        Session["Email"] = objUserModel.UserEmail;
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

            return RedirectToAction("ViewUser", "User");
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

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}