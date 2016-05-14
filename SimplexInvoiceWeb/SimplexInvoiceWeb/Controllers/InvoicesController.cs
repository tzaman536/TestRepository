using log4net;
using SimplexInvoiceBL;
using SimplexInvoiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplexInvoiceWeb.Controllers
{
    public class InvoicesController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MyCompanyController));


        SimplexInvoiceHelper helper = new SimplexInvoiceHelper();
        LogisticsCompanyHandler ch = new LogisticsCompanyHandler();
        ClientsCompanyHandler clientCompanyHandler = new ClientsCompanyHandler();
        LogisticsCompany lc;

        // GET: Invoices
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            ClientCompany c = null;

            if (c == null)
            {
                c = new ClientCompany();
                c.CompanyName = string.Empty;
                c.ContactPerson = string.Empty;
                c.AddressLine1 = string.Empty;
                c.AddressLine2 = string.Empty;
                c.City = string.Empty;
                c.State = "NY";
                c.Zip = string.Empty;
                c.Email = string.Empty;
                c.MobileNumber = string.Empty;
                c.OfficeNumber = string.Empty;
                c.FaxNumber = string.Empty;
            }

            lc = ch.GetCompanyRegisteredByUser(User.Identity.Name);
            if (lc != null)
            {
                c.WeightRate = lc.WeightRate;
                c.ComplimentaryWeight = lc.ComplimentaryWeight;
                c.BasePickupCharge = lc.BasePickupCharge;
            }


            ViewData["PU_FORM"] = c;



            ViewData["MY_COMPANY"] = lc;

            return View();
        }
    }
}