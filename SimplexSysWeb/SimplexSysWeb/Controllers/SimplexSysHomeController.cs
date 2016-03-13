using EZFactorModel.Ref;
using Kendo.Mvc.UI;
using PhenixBL;
using PhenixModel.Ref;
using PhenixTools.Logger;
using PhenixTools.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SimplexSysWeb.Controllers
{
    public class SimplexSysHomeController : Controller
    {
        // GET: SimplexSysHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Leave us a message.";
            ClientContactInfo p = new ClientContactInfo();
            p.CompanyName = string.Empty;
            p.FirstName = string.Empty;
            p.LastName = string.Empty;
            p.Email = string.Empty;
            p.MobileNumber = string.Empty;
            p.OfficeNumber = string.Empty;
            p.Message = string.Empty;

            SimplexLogger.Info("Returning view from HomeController Contact()");

            return View(p);
        }


        public ActionResult ITInfrastructure()
        {
            return View();
        }

        public ActionResult SoftwareConsulting()
        {
            return View();
        }

        public ActionResult CustomSoftware()
        {
            return View();
        }




        [HttpPost]
        public ActionResult ContactUs([DataSourceRequest]DataSourceRequest request, string jsonStringCompany)
        {

            int totalClientCount = 0;
            var json_serializer = new JavaScriptSerializer();
            ClientContactInfo c = json_serializer.Deserialize<ClientContactInfo>(jsonStringCompany);

            daContactDetail.SaveClientContactDetail(c);

            IEnumerable<ClientContactInfo> clients = daContactDetail.GetClientContactDetail();
            if (clients != null)
                totalClientCount = clients.Count();

            if (!c.Email.Contains("@") || !c.Email.Contains("."))
            {
                return Json(new { success = true, message = string.Format("Invalid email address: {0}", c.Email) }, JsonRequestBehavior.AllowGet);
            }

            try
            {

                PhenixMail.SendMail(string.Format("Client {0} requesting information ", c.FirstName), c.Message, ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
            }
            catch (Exception ex)
            {
                PhenixMail.SendMail("HomeController.ContactUs()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
            }


            return Json(new { success = true, message = string.Format("Thanks for contacting us. Someone will get back to you soon.") }, JsonRequestBehavior.AllowGet);
        }


    }


}