using Kendo.Mvc.UI;
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
        // GET: MyCompany
        public ActionResult Index()
        {
            Company c = new Company();
            c.CompanyName = string.Empty;
            c.FirstName = string.Empty;
            c.LastName = string.Empty;
            c.Email = string.Empty;
            c.MobileNumber = string.Empty;
            c.OfficeNumber = string.Empty;
            c.Message = string.Empty;

            return View(c);
        }

        [HttpPost]
        public ActionResult ContactUs([DataSourceRequest]DataSourceRequest request, string jsonStringCompany)
        {

            int totalClientCount = 0;
            var json_serializer = new JavaScriptSerializer();
            Company c = json_serializer.Deserialize<Company>(jsonStringCompany);

            //daContactDetail.SaveClientContactDetail(c);

            //IEnumerable<ClientContactInfo> clients = daContactDetail.GetClientContactDetail();
            //if (clients != null)
            //    totalClientCount = clients.Count();

            //if (!c.Email.Contains("@") || !c.Email.Contains("."))
            //{
            //    return Json(new { success = true, message = string.Format("Invalid email address: {0}", c.Email) }, JsonRequestBehavior.AllowGet);
            //}

            //try
            //{

            //    PhenixMail.SendMail(string.Format("Client {0} requesting information ", c.FirstName), c.Message, ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
            //}
            //catch (Exception ex)
            //{
            //    PhenixMail.SendMail("HomeController.ContactUs()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
            //}


            return Json(new { success = true, message = string.Format("Thanks for contacting us. Someone will get back to you soon.") }, JsonRequestBehavior.AllowGet);
        }

    }
}