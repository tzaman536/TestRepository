﻿using Kendo.Mvc.Extensions;
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
    public class MyCompanyController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MyCompanyController));


        SimplexInvoiceHelper helper = new SimplexInvoiceHelper();
        LogisticsCompanyHandler ch = new LogisticsCompanyHandler();
        // GET: MyCompany
        public ActionResult Index()
        {

            logger.Info("Simplex Invoice MyCompanyController.Index()");

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            LogisticsCompany c = (LogisticsCompany)ch.GetCompanyRegisteredByUser(User.Identity.Name);
            if (c == null)
            {
                c = new LogisticsCompany();
                c.CompanyName = string.Empty;
                c.ContactPerson = string.Empty;
                c.AddressLine1 = string.Empty;
                c.AddressLine2 = string.Empty;
                c.City = string.Empty;
                c.State = "Choose one";
                c.Zip = string.Empty;
                c.Email = string.Empty;
                c.MobileNumber = string.Empty;
                c.OfficeNumber = string.Empty;
                c.FaxNumber = string.Empty;
                c.ComplimentaryWeight = 100;
                c.WeightRate = 2;
                c.BasePickupCharge = 25;
            }

            

            return View(c);
        }

        [HttpPost]
        public ActionResult SaveCompanyInfo([DataSourceRequest]DataSourceRequest request, string jsonStringCompany)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            string message = "Company saved.";
            var json_serializer = new JavaScriptSerializer();
            LogisticsCompany c = json_serializer.Deserialize<LogisticsCompany>(jsonStringCompany);
            c.CreatedBy = User.Identity.Name;
            logger.InfoFormat("Saving company...");
            try
            {
                var existingCompany = (LogisticsCompany)ch.GetCompanyRegisteredByUser(User.Identity.Name);
                if (existingCompany == null)
                {
                    c.CompanyId = ch.Add(c, User.Identity.Name);
                }
                else
                {
                    c.CompanyId = existingCompany.CompanyId;
                    logger.InfoFormat("Company exists. Updating company.");
                    c.ModifiedBy = User.Identity.Name;
                    logger.InfoFormat("{0} rows updated.", ch.Update(c));
                }

                logger.InfoFormat("Company saved.");

            }
            catch (Exception ex)
            {
                message = string.Format("Failed to save company. Error: {0}",ex.Message);
                logger.Fatal(ex);
            }

            
            return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
        }


        

        public ActionResult GetStates()
        {
            return Json(SimplexInvoiceHelper.GetStates(), JsonRequestBehavior.AllowGet);
        }

    }
}