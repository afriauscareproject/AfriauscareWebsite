using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.ContactInformation;
using Afriauscare.BusinessLayer.Error;
using Afriauscare.DataBaseLayer.Shared;
using Afriauscare.DataBaseLayer;

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
            return View();
        }

        public ActionResult ViewContactInformation()
        {
            List<ContactInformationModel> list = new List<ContactInformationModel>();
            ContactInformationDAO objDAO = new ContactInformationDAO();
            list = objDAO.GetContactInformationAll();

            return View(list);
        }

        public ActionResult CreateContactInformation()
        {
            ContactInformationModel objModel = new ContactInformationModel();
            StatesDAO objStateDao = new StatesDAO();
            List<SelectListItem> emptyList = new List<SelectListItem>();
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

        [HttpPost]
        public ActionResult CreateContactInformation(ContactInformationModel objContactModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInformationDAO objContactDAO = new ContactInformationDAO();
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
                        Postcode = string.Empty
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
        public ActionResult GetSuburbs(string state_id)
        {

            var suburbsDao = new SuburbsDAO();

            IEnumerable<SelectListItem> suburbs = suburbsDao.GetSuburbs(state_id);
            return Json(suburbs, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult ModifyContactInformation(int ContactId)
        {
            ContactInformationDAO objContactDAO = new ContactInformationDAO();
            try
            {
                var objContactModel = objContactDAO.GetContactInformationbyId(ContactId);

                StatesDAO objStateDao = new StatesDAO();
                List<SelectListItem> emptyList = new List<SelectListItem>();
                var first_item = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Suburb ---"
                };
                emptyList.Add(first_item);

                objContactModel.States = objStateDao.GetStates();
                objContactModel.States.Where(s => s.Value == objContactModel.State_id);
                objContactModel.Suburbs = emptyList;

                return View(objContactModel);
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