using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.ContactInformation;
using Afriauscare.BusinessLayer.Error;
using Afriauscare.BusinessLayer.BankInformation;
using Afriauscare.DataBaseLayer.Shared;
using Afriauscare.DataBaseLayer.ContactInformation;
using Afriauscare.DataBaseLayer.BankInformation;
using Afriauscare.BusinessLayer.Shared;

namespace AfriauscareWebsite.Controllers
{
    public class HomeAdminPortalController : Controller
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

        // GET: HomeAdminPortal
        public ActionResult Index()
        {
            LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
            List<LogUserActivityModel> objLogActivityList = objLogUserDao.GetLogUserInformation();

            return View(objLogActivityList);
        }

        public ActionResult ViewContactInformation()
        {
            ContactInformationDAO objDAO = new ContactInformationDAO();
            try
            {
                List<ContactInformationModel> list = objDAO.GetContactInformationAll();
                return View(list);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

        }

        public ActionResult CreateContactInformation()
        {
            ContactInformationModel objModel = new ContactInformationModel();
            StatesDAO objStateDao = new StatesDAO();
            List<SelectListItem> emptyList = new List<SelectListItem>();
            try
            {
                var first_item = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Suburb ---"
                };
                emptyList.Add(first_item);

                objModel.States = objStateDao.GetStates();
                objModel.Suburbs = emptyList;
                return View(objModel);
            }
            catch(Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            
        }

        [HttpPost]
        public ActionResult CreateContactInformation(ContactInformationModel objContactModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInformationDAO objContactDAO = new ContactInformationDAO();
                    if (objContactModel.Is_default)
                    {
                        objContactDAO.ClearIsDefaultField();
                    }
                    objContactDAO.CreateContactInformation(objContactModel);

                    ModelState.Clear();
                    ContactInformationModel objEmptyContactModel = new ContactInformationModel()
                    {
                        Email_address = string.Empty,
                        Phone_number = string.Empty,
                        Mobile_number = string.Empty,
                        Fax_number = string.Empty,
                        Contact_address = string.Empty,
                        State_id = string.Empty,
                        Suburb_id = string.Empty,
                        Postcode = string.Empty,
                        Is_default = false
                    };

                    StatesDAO objStateDao = new StatesDAO();
                    List<SelectListItem> emptyList = new List<SelectListItem>();
                    var first_item = new SelectListItem()
                    {
                        Value = "",
                        Text = "--- Select Suburb ---"
                    };
                    emptyList.Add(first_item);

                    objEmptyContactModel.States = objStateDao.GetStates();
                    objEmptyContactModel.Suburbs = emptyList;

                    LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                    LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                    {
                        User_id = Int16.Parse(Session["UserId"].ToString()),
                        Module_Name = "ContactInformation",
                        Action_Excuted = "Create",
                        Datetime_action = DateTime.Now
                    };

                    objLogUserDao.CreateLogUserActivity(objLogUserModel);

                    TempData["ContactInformationAlertMessage"] = "Contact Information Created ...";
                    return View("CreateContactInformation", objEmptyContactModel);
                }
                else
                {
                    StatesDAO objStateDao = new StatesDAO();
                    SuburbsDAO objSuburbs = new SuburbsDAO();

                    objContactModel.States = objStateDao.GetStates();
                    if (objContactModel.State_id == null || objContactModel.State_id == string.Empty)
                    {
                        objContactModel.Suburbs = objSuburbs.GetSuburbs("0");
                        objContactModel.Suburb_id = null;
                    }
                    else
                    {
                        objContactModel.Suburbs = objSuburbs.GetSuburbs(objContactModel.State_id);
                    }
                    
                    return View("CreateContactInformation", objContactModel);
                }
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }


        }

        [HttpGet]
        public ActionResult ModifyContactInformation(int ContactId)
        {
            ContactInformationDAO objContactDAO = new ContactInformationDAO();
            try
            {
                var objContactModel = objContactDAO.GetContactInformationbyId(ContactId);

                StatesDAO objStateDao = new StatesDAO();
                SuburbsDAO objSuburbDao = new SuburbsDAO();

                objContactModel.States = objStateDao.GetStates();
                objContactModel.Suburbs = objSuburbDao.GetSuburbs(objContactModel.State_id);


                return View(objContactModel);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

        [HttpPost]
        public ActionResult ModifyContactInformation(ContactInformationModel objContactModel)
        {
            ContactInformationDAO objContactDao = new ContactInformationDAO();
            StatesDAO objStateDao = new StatesDAO();
            SuburbsDAO objSuburbDao = new SuburbsDAO();

            try
            {
                if (ModelState.IsValid)
                {
                    if (objContactModel.Is_default)
                    {
                        objContactDao.ClearIsDefaultField();
                    }

                    objContactDao.ModifyContactInformation(objContactModel);

                    ModelState.Clear();


                    ContactInformationModel objEmptyModel = new ContactInformationModel()
                    {
                        Contact_id = 0,
                        Email_address = string.Empty,
                        Phone_number = string.Empty,
                        Mobile_number = string.Empty,
                        Fax_number = string.Empty,
                        Contact_address = string.Empty,
                        State_id = string.Empty,
                        States = objStateDao.GetStates(),
                        Suburb_id = string.Empty,
                        Suburbs = objSuburbDao.GetSuburbs("0"),
                        Postcode = string.Empty
                    };

                    LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                    LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                    {
                        User_id = Int16.Parse(Session["UserId"].ToString()),
                        Module_Name = "ContactInformation",
                        Action_Excuted = "Modify",
                        Datetime_action = DateTime.Now
                    };

                    objLogUserDao.CreateLogUserActivity(objLogUserModel);

                    TempData["ContactInformationAlertMessage"] = "Contact Information Modifed Successfully...";

                    return View("ModifyContactInformation", objEmptyModel);
                }
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            objContactModel.States = objStateDao.GetStates();
            objContactModel.Suburbs = objSuburbDao.GetSuburbs(objContactModel.State_id);

            return View("ModifyContactInformation",objContactModel);
        }

        public ActionResult ViewBankInformation()
        {
            BankInformationDAO objDAO = new BankInformationDAO();

            try
            {
                List<BankInformationModel> list = objDAO.GetBankInformationAll();
                return View(list);
            }
            catch(Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            
        }

        public ActionResult CreateBankInformation()
        {
            BankInformationModel objModel = new BankInformationModel();
            BanksDAO objBanksDao = new BanksDAO();

            try
            {
                objModel.Banks = objBanksDao.GetBanks();
                return View(objModel);
            }
            catch(Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
            
        }

        [HttpPost]
        public ActionResult CreateBankInformation(BankInformationModel objBankModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BankInformationDAO objBankDAO = new BankInformationDAO();
                    if (objBankModel.Is_default)
                    {
                        objBankDAO.ClearIsDefaultField();
                    }
                    objBankDAO.CreateBankInformation(objBankModel);

                    ModelState.Clear();
                    BankInformationModel objEmptyBankModel = new BankInformationModel()
                    {
                        Abn_number = string.Empty,
                        Bsb_number = string.Empty,
                        Account_number = string.Empty,
                        Is_default = false
                    };

                    BanksDAO objBankDao = new BanksDAO();

                    objEmptyBankModel.Banks = objBankDao.GetBanks();

                    LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                    LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                    {
                        User_id = Int16.Parse(Session["UserId"].ToString()),
                        Module_Name = "BankInformation",
                        Action_Excuted = "Create",
                        Datetime_action = DateTime.Now
                    };

                    objLogUserDao.CreateLogUserActivity(objLogUserModel);

                    TempData["BankInformationAlertMessage"] = "Bank Information Created ...";
                    return View("CreateBankInformation", objEmptyBankModel);
                }
                else
                {
                    BanksDAO objBankDao = new BanksDAO();

                    objBankModel.Banks = objBankDao.GetBanks();

                    return View("CreateBankInformation", objBankModel);
                }
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }


        }

        [HttpGet]
        public ActionResult ModifyBankInformation(int BankInformationId)
        {
            BankInformationDAO objBankDAO = new BankInformationDAO();
            try
            {
                var objBankModel = objBankDAO.GetBankInformationbyId(BankInformationId);

                BanksDAO objBankDao = new BanksDAO();

                objBankModel.Banks = objBankDao.GetBanks();

                return View(objBankModel);
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }
        }

        [HttpPost]
        public ActionResult ModifyBankInformation(BankInformationModel objBankModel)
        {
            BankInformationDAO objBankInformationDao = new BankInformationDAO();
            BanksDAO objBankDao = new BanksDAO();

            try
            {
                if (ModelState.IsValid)
                {
                    if (objBankModel.Is_default)
                    {
                        objBankInformationDao.ClearIsDefaultField();
                    }

                    objBankInformationDao.ModifyBankInformation(objBankModel);

                    ModelState.Clear();

                    BankInformationModel objEmptyModel = new BankInformationModel()
                    {
                        Bank_information_id = 0,
                        Bank_id = 0,
                        Abn_number = string.Empty,
                        Account_number = string.Empty,
                        Bsb_number = string.Empty,
                        Is_default = false
                    };

                    LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                    LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                    {
                        User_id = Int16.Parse(Session["UserId"].ToString()),
                        Module_Name = "BankInformation",
                        Action_Excuted = "Modify",
                        Datetime_action = DateTime.Now
                    };

                    objLogUserDao.CreateLogUserActivity(objLogUserModel);

                    TempData["BankInformationAlertMessage"] = "Bank Information Modifed Successfully...";
                    objEmptyModel.Banks = objBankDao.GetBanks();

                    return View("ModifyBankInformation", objEmptyModel);
                }
            }
            catch (Exception ex)
            {
                ErrorModel objErrorModel = new ErrorModel();
                objErrorModel.ErrorMessage = ex.Message;
                return RedirectToAction("Error", "Error", objErrorModel);
            }

            objBankModel.Banks = objBankDao.GetBanks();

            return View("ModifyBankInformation", objBankModel);
        }

        [HttpPost]
        public JsonResult DeleteBankInformationJson(int BankInformationId)
        {
            bool result = false;
            BankInformationDAO objBankInformationDAO = new BankInformationDAO();
            try
            {
                objBankInformationDAO.DeleteBankInformation(BankInformationId);
                result = true;

                LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                {
                    User_id = Int16.Parse(Session["UserId"].ToString()),
                    Module_Name = "BankInformation",
                    Action_Excuted = "Delete",
                    Datetime_action = DateTime.Now
                };

                objLogUserDao.CreateLogUserActivity(objLogUserModel);

                TempData["BankInformationAlertMessage"] = "Bank Information Deleted Successfully...";

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteContactInformationJson(int ContactInformationId)
        {
            bool result = false;
            ContactInformationDAO objContactInformationDAO = new ContactInformationDAO();
       
            try
            {
                objContactInformationDAO.DeleteContactInformation(ContactInformationId);
                result = true;

                LogUserActivityDAO objLogUserDao = new LogUserActivityDAO();
                LogUserActivityModel objLogUserModel = new LogUserActivityModel()
                {
                    User_id = Int16.Parse(Session["UserId"].ToString()),
                    Module_Name = "ContactInformation",
                    Action_Excuted = "Delete",
                    Datetime_action = DateTime.Now
                };

                objLogUserDao.CreateLogUserActivity(objLogUserModel);

                TempData["ContactInformationAlertMessage"] = "Contact Information Deleted Successfully...";

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetSuburbs(string state_id)
        {

            var suburbsDao = new SuburbsDAO();

            try
            {
                IEnumerable<SelectListItem> suburbs = suburbsDao.GetSuburbs(state_id);
                return Json(suburbs, JsonRequestBehavior.AllowGet);
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