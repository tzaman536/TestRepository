using Kendo.Mvc.UI;
using log4net;
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
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
    public class MyClientsController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MyCompanyController));


        SimplexInvoiceHelper helper = new SimplexInvoiceHelper();
        LogisticsCompanyHandler ch = new LogisticsCompanyHandler();
        ClientsCompanyHandler clientCompanyHandler = new ClientsCompanyHandler();



        // GET: MyClients
        public ActionResult Index()
        {
            logger.Info("Simplex Invoice MyClients. Index()");

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

            LogisticsCompany lc = ch.GetCompanyRegisteredByUser(User.Identity.Name);
            if(lc != null)
            {
                c.WeightRate = lc.WeightRate;
                c.ComplimentaryWeight = lc.ComplimentaryWeight;
                c.BasePickupCharge = lc.BasePickupCharge;
            }


            ViewData["PU_FORM"] = c;

            return View();
        }

        [HttpPost]
        public ActionResult SaveClientInfo([DataSourceRequest]DataSourceRequest request, string clientCompanyString)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            string message = "Company saved.";
            var json_serializer = new JavaScriptSerializer();
            ClientCompany c = json_serializer.Deserialize<ClientCompany>(clientCompanyString);
            c.SimplexInvoiceUserId = User.Identity.Name;
            c.CreatedBy = User.Identity.Name;
            logger.InfoFormat("Saving company...");
            try
            {
                var existingCompany = clientCompanyHandler.GetCompanyByName(c.CompanyName);
                if (existingCompany == null)
                    c.CompanyId = clientCompanyHandler.Add(c);
                else
                {
                    c.CompanyId = existingCompany.CompanyId;
                    logger.InfoFormat("Company exists. Updating client company.");
                    c.ModifiedBy = User.Identity.Name;
                    logger.InfoFormat("{0} rows updated.", clientCompanyHandler.Update(c));
                }

                logger.InfoFormat("Company saved.");

            }
            catch (Exception ex)
            {
                message = string.Format("Failed to save company. Error: {0}", ex.Message);
                logger.Fatal(ex);
            }


            return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
        }


    }
}