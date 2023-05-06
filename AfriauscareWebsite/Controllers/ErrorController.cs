using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afriauscare.BusinessLayer.Error;

namespace AfriauscareWebsite.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error(ErrorModel objErrorModel)
        {
            return View(objErrorModel);
        }
    }
}