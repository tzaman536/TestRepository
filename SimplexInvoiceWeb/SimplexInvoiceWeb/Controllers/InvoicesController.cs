using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplexInvoiceWeb.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult Index()
        {
            return View();
        }
    }
}