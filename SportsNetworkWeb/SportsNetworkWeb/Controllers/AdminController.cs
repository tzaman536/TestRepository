using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplexInvoiceWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string homeController = "Home";

            if (!User.IsInRole("AccountAdmin"))
            {
                return RedirectToAction("Index", homeController);
            }
            return View();
        }
    }
}