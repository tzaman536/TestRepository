using Kendo.Mvc.UI;
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
        ClientsCompanyHandler cch = new ClientsCompanyHandler();
        LogisticsCompany lc;
        JobTicketHandler jth = new JobTicketHandler();

        // GET: Invoices
        public ActionResult Index(int? jobTicketId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            

            ViewData["JOB_TICKET_ID"] = jobTicketId;



            return View();
        }

        [HttpGet]
        public ActionResult GetInvoice([DataSourceRequest]DataSourceRequest request, int jobTicketId)
        {
            Invoice invoice = new Invoice();
            lc = ch.GetCompanyRegisteredByUser(User.Identity.Name);
            var jobTicket = jth.GetJobTicket(jobTicketId);
            invoice.MyCompanyAddress = string.Format("{0}<br />{1}<br />{2}, {3} {4} <br />", lc.CompanyName,lc.AddressLine1,lc.City,lc.State,lc.Zip);
            invoice.MyCompanyContactInfo = string.Format("TEL: {0} <br /> FAX: {1} <br />", lc.MobileNumber, lc.FaxNumber);
            invoice.JobDate = string.Format("DATE: {0}",jobTicket.JobDate.ToString("MM/dd/yyyy"));
            invoice.JobNumber = string.Format("JOB NUMBER: {0}", jobTicket.JobTicketId);
            var clientCompany =  cch.GetClientCompanyById(jobTicket.ClientCompanyId, lc);
            
            invoice.BillTo = string.Format(@"BILL TO :&nbsp;&nbsp;{0}
                                            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            {1}
                                            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            {2}&nbsp;&nbsp;{3}&nbsp;{4}<br />", clientCompany.BillToName, clientCompany.BillToAddressLine1, clientCompany.BillToCity,clientCompany.State,clientCompany.BillToZip);

            invoice.PUFromAddress = string.Format("P/U FROM : </br> {0} <br />", jobTicket.PickupFrom.Replace("\n", "<br />"));
            invoice.PUFromContactInfo = string.Format("CONTACT : {0} <br /> PHONE: {1} <br />", jobTicket.PickupFromContact,jobTicket.PickupFromPhone);
            invoice.DeliverToAddress = string.Format("DELIVER TO : </br> {0} <br />", jobTicket.DeliverTo.Replace("\n", "<br />"));
            invoice.DeliverToContactInfo = string.Format("CONTACT : {0} <br /> PHONE: {1} <br />", jobTicket.DeliverToContact, jobTicket.DeliverToPhone);


            return Json(new { success = true, message = invoice }, JsonRequestBehavior.AllowGet);
        }

    }

}