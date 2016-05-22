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
        public decimal InputQuantity { get; set; }
        public decimal InputWeight { get; set; }
        public decimal InputBasePickupCharge { get; set; }
        public decimal InputMilage { get; set; }
        public decimal InputToll { get; set; }
        public decimal InputFuelSurcharge { get; set; }
        public decimal InputMiscCharges { get; set; }
        public string InputClient { get; set; }
        public decimal Charge { get; set; }

        public void Calc(decimal weightRate, decimal complimentaryWeight)
        {
            decimal weight = InputWeight - complimentaryWeight;
            if (weight < 0)
                weight = 0;

            Charge = weight * weightRate + InputBasePickupCharge + InputMilage + InputToll + InputFuelSurcharge + InputMiscCharges;
        }

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
        public ActionResult CalcTotalCharge([DataSourceRequest]DataSourceRequest request, string inputChargeParameters)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var json_serializer = new JavaScriptSerializer();
            TotalCharge chargeInput = json_serializer.Deserialize<TotalCharge>(inputChargeParameters);


            if (string.IsNullOrEmpty(chargeInput.InputClient))
                return Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);

            if (lc == null)
                lc = lch.GetCompanyRegisteredByUser(User.Identity.Name);

            ClientCompany c = cch.GetCompanyByName(chargeInput.InputClient, lc);

            chargeInput.Calc(c.WeightRate, c.ComplimentaryWeight);
            
            


            //decimal totalCharges = inputQuantity * inputWeight * 

            return Json(new { success = true, message = chargeInput }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveJobTicket([DataSourceRequest]DataSourceRequest request, string inputJobTicket)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var json_serializer = new JavaScriptSerializer();
            JobTicket ticket = json_serializer.Deserialize<JobTicket>(inputJobTicket);


            
            if (lc == null)
                lc = lch.GetCompanyRegisteredByUser(User.Identity.Name);


            // Save to db here
            // Assign job ticket number to the object
            // return jobTicket

            return Json(new { success = true, message = ticket }, JsonRequestBehavior.AllowGet);
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