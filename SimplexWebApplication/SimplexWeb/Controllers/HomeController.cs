using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZFactorModel.Ref;
using Kendo.Mvc.UI;
using System.Web.Script.Serialization;
using PhenixModel.Ref;
using System.Net.Mail;
using System.ComponentModel;
using System.Web.Helpers;
using System.Net;
using System.Configuration;
using PhenixTools.Mail;
using PhenixBL;
using log4net;
using PhenixTools.Logger;


namespace EZFactor1.Controllers
{
      
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            SimplexLogger.PrintLogTest();
            SimplexLogger.Info(string.Format("Returning view from HomeController Index(). Current user is {0}:",Environment.UserName));

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            SimplexLogger.Info("Returning view from HomeController About()");

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

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
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

            
            return Json(new { success = true, message = string.Format("Total client count:  {0}", totalClientCount) }, JsonRequestBehavior.AllowGet);
        }
    }
}
