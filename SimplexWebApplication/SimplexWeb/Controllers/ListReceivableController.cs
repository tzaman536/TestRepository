using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Kendo.Mvc.UI;

namespace EZFactor1.Controllers
{
    public class ListReceivableController : Controller
    {
        //
        // GET: /ListReceivable/

        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated) { Response.Redirect(Url.Action("LogIn", "Account")); }

            return View();
        }

        public ActionResult IsCompanyRegistered([DataSourceRequest]DataSourceRequest request)
        {
            bool registered = false;
            string message = string.Format("Welcome! {0}",WebSecurity.CurrentUserName);
            // Add code to check if company is registered

            if (!registered)
                message = "Please register your company.";

            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);


        }


    }
}
