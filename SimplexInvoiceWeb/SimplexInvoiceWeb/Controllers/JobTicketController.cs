using Kendo.Mvc.Extensions;
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
        JobTicketHandler jth = new JobTicketHandler();


        // GET: JobTicket
        public ActionResult Index(int? jobTicketId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["JOB_TICKET_ID"] = jobTicketId;
            return View();
        }

        public ActionResult EditTicket(int jobTicketId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }


        public ActionResult SearchTicket()
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

            var clientCompany = cch.GetCompanyByName(ticket.ClientName, lc);
            ticket.ClientCompanyId = clientCompany.ClientCompanyId;


            if (ticket.JobTicketId == 0)
            {

                ticket.CreatedBy = User.Identity.Name;
                ticket.CompanyId = lc.CompanyId;
                
                ticket.JobTicketId = jth.Add(ticket, User.Identity.Name);
            }
            else
            {
                ticket.ModifiedBy = User.Identity.Name;
                jth.Update(ticket);

            }
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

        public ActionResult Search_Read([DataSourceRequest] DataSourceRequest request, string jobDate, string clientName)
        {

            if (lc == null)
                lc = lch.GetCompanyRegisteredByUser(User.Identity.Name);



            IEnumerable<JobTicket> result = jth.GetAllTickets(lc);
            if (result == null)
                result = new List<JobTicket>();

            if(!jobDate.Equals("UNSELECTED"))
            {
                result = result.Where(x => x.JobDate.ToString("d").Equals(jobDate));
            }

            if (!clientName.Equals("UNSELECTED"))
            {
                result = result.Where(x => x.ClientName.Equals(clientName));
            }


            return Json(result.ToDataSourceResult(request));
        }


        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {

            if (lc == null)
                lc = lch.GetCompanyRegisteredByUser(User.Identity.Name);



            IEnumerable<JobTicket> result = jth.GetTodaysTickes(lc);
            if (result == null)
                result = new List<JobTicket>();

            return Json(result.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult GetJobTicketInfo([DataSourceRequest]DataSourceRequest request, int jobTicketId)
        {

            
            return Json(new { success = true, message = jth.GetJobTicket(jobTicketId) }, JsonRequestBehavior.AllowGet);
        }



    }
}