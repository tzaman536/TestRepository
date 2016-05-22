using Kendo.Mvc.UI;
using SimplexInvoiceBL;
using SimplexInvoiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SimplexInvoiceWeb.Controllers
{
    public class TotalCharge
    {
        public int Charge { get; set; }
    }

    public class JobTicketController : Controller
    {
        LogisticsCompanyHandler lch = new LogisticsCompanyHandler();
        ClientsCompanyHandler cch = new ClientsCompanyHandler();
        LogisticsCompany lc;

        // GET: JobTicket
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public ActionResult CalcTotalCharge([DataSourceRequest]DataSourceRequest request, decimal inputQuantity, decimal inputWeight, decimal inputBasePickupCharge, decimal inputMilage
                                            ,decimal inputToll, decimal inputFuelSurcharge,decimal inputMiscCharges, string inputClient)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            if (string.IsNullOrEmpty(inputClient))
                return Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);

            if (lc == null)
                lc = lch.GetCompanyRegisteredByUser(User.Identity.Name);

            ClientCompany c = cch.GetCompanyByName(inputClient, lc);

            TotalCharge tc = new TotalCharge() { Charge = 600 };
            //decimal totalCharges = inputQuantity * inputWeight * 

            return Json(new { success = true, message = tc }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetClientDefaults([DataSourceRequest]DataSourceRequest request, string inputClient)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            if (string.IsNullOrEmpty(inputClient))
                return Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);

            if (lc == null)
                lc = lch.GetCompanyRegisteredByUser(User.Identity.Name);

            ClientCompany c = cch.GetCompanyByName(inputClient, lc);

            

            return Json(new { success = true, message = c }, JsonRequestBehavior.AllowGet);
        }

    }
}