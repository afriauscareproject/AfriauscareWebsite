using Afriauscare.BusinessLayer.Error;
using Afriauscare.BusinessLayer.Shared;
using Afriauscare.BusinessLayer.User;
using Afriauscare.DataBaseLayer;
using Afriauscare.DataBaseLayer.Shared;
using System;
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

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDAO objUserDao = new UserDAO();

                    if (objUserDao.getUserbyEmail(model))
                    {
                        string temporaryPassword = Membership.GeneratePassword(8, 1);
                        string FromEmail = string.Empty;
                        string FromEmailPassword = string.Empty;
                        string SMTPPort = string.Empty;
                        string Host = string.Empty;
                        string To = model.UserEmail;

                        string subject = "Afriauscare - Password Reset Request";
                        string body = "<b> Please find your temporary password. </b> <br/>" + temporaryPassword;

                        EmailManager.AppSettings(out FromEmail, out FromEmailPassword, out SMTPPort, out Host);
                        EmailManager.SendEmail(FromEmail, subject, body, To, FromEmail, FromEmailPassword, SMTPPort, Host);
                        TempData["ConfirmationMessageEmail"] = "We have sent you an email with a temporary password.";

                        model.UserId = objUserDao.getUserIdByEmail(model.UserEmail);
                        model.UserPassword = temporaryPassword;
                        objUserDao.ChangeUserPassword(model);

                        return RedirectToAction("UpdatePasswordDetails", "Session", new { emailAddress = model.UserEmail });
                    }
                    else
                    {
                        return View();
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

        [HttpGet]
        public ActionResult UpdatePasswordDetails(string emailAddress)
        {
            try
            {
                UserDAO objUserDao = new UserDAO();
                ForgotPasswordModel model = new ForgotPasswordModel();
                model.UserEmail = emailAddress;
                model.UserId = objUserDao.getUserIdByEmail(emailAddress);

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            
        }

        [HttpPost]
        public ActionResult UpdatePasswordDetails(ForgotPasswordModel model)
        {
            try
            {
                UserDAO objUserDao = new UserDAO();

                if (!objUserDao.ValidateTemporaryPassword(model))
                {
                    ModelState.AddModelError("TemporaryPassword", "The temporary password entered does not match. Please check again.");
                }

                if (ModelState.IsValid)
                {
                    model.UserId = objUserDao.getUserIdByEmail(model.UserEmail);
                    objUserDao.ActivateUser(model);

                    LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                    LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                    {
                        User_id = model.UserId,
                        Module_Name = "User",
                        Action_Excuted = "PasswordChanged",
                        Datetime_action = DateTime.Now
                    };

                    objLogUserDao.CreateLogUserActivity(objLogUserModel);
                    TempData["ConfirmationMessageEmail"] = "Password updated successfully. Please go back to Login page.";
                    ModelState.Clear();

                    return View();
                }
                else
                {
                    return View();
                }
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