using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.ContactInformation;

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

            return View(list);
        }

        public ActionResult CreateContactInformation()
        {
            return View();
        }
    }
}